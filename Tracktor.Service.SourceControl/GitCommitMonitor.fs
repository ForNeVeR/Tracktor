namespace Tracktor.Service.SourceControl

open LibGit2Sharp
open System
open System.IO
open Tracktor.Contracts

type GitCommitMonitor() =
    let event = Event<_>()
    let raiseEvent (commit: LibGit2Sharp.Commit) =
        event.Trigger { Revision = commit.Id.Sha
                        Author = commit.Author.Name }
    
    let repository = ref None

    member __.NewCommit = event.Publish
    member __.Start() =
        let url = "https://github.com/ForNeVeR/Tracktor.git"
        let directory = Path.GetTempFileName()    

        let clone = Repository.Clone(url, directory)
        let repo = new Repository(clone)
        repository := Some repo

        do repo.Commits
        |> Seq.iter raiseEvent

        // TODO: Start timer and check the repo periodically.

    interface IDisposable with
        member __.Dispose() =
            match !repository with
            | Some(repo) -> repo.Dispose()
            | None -> ()

