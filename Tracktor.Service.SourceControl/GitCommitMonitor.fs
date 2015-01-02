namespace Tracktor.Service.SourceControl

open LibGit2Sharp
open ReactiveGit
open System
open System.IO
open System.Reactive
open System.Threading
open System.Threading.Tasks
open Tracktor.Contracts
open Tracktor.Service.Common

type GitCommitMonitor(configuration: ServiceConfiguration, parameters : ProjectParameters) =
    let delay = TimeSpan.FromMinutes 5.0

    let event = Event<_>()
    let raiseEvent (commit: LibGit2Sharp.Commit) =
        event.Trigger { Revision = commit.Id.Sha
                        Author = commit.Author.Name }

    let tokenSource = new CancellationTokenSource()
    let checkTask = ref None

    let observe (commitSource : IObservable<LibGit2Sharp.Commit>) : Async<unit> =
        let taskSource = TaskCompletionSource()
        let observer = Observer.Create(raiseEvent, fun () -> taskSource.SetResult())
        commitSource.Subscribe observer |> ignore
        Async.AwaitTask taskSource.Task

    let checker path =
        let emptyObserver = Observer.Create<string * int>(fun _ -> ())
        async {
            while not Async.DefaultCancellationToken.IsCancellationRequested do
                use repo = new ObservableRepository(path)
                do! observe (repo.Pull emptyObserver |> Observable.map (fun mergeInfo -> mergeInfo.Commit))
                do! Async.Sleep(int delay.TotalMilliseconds)
        }

    let startTask path =
        let token = tokenSource.Token
        let task = Async.StartAsTask(checker path, cancellationToken = token)
        task

    interface ICommitMonitor with
        member __.NewCommit = event.Publish
        member __.Start() =
            let directory = Path.Combine(configuration.DataDirectory, parameters.Name)
            if not <| Directory.Exists directory
            then
                ignore <| Repository.Clone(parameters.Url, directory)
                use repo = new Repository(directory)
                Seq.iter raiseEvent repo.Commits

            let task = startTask directory
            checkTask := Some task

    interface IDisposable with
        member __.Dispose() =
            tokenSource.Cancel()

            match !checkTask with
            | Some(task) ->
                task.Wait()
            | None -> ()

            tokenSource.Dispose()
