namespace Tracktor.Service.SourceControl

open LibGit2Sharp
open System
open System.IO
open Tracktor.Contracts
open Tracktor.Service.Common

type GitCommitMonitor(configuration: ServiceConfiguration, projectName: String, url: String) =
    let event = Event<_>()
    let raiseEvent (commit: LibGit2Sharp.Commit) =
        event.Trigger { Revision = commit.Id.Sha
                        Author = commit.Author.Name }
    
    let repository = ref None

    member __.NewCommit = event.Publish
    member __.Start() =
        let directory = Path.Combine(configuration.DataDirectory, projectName)
        if not <| Directory.Exists directory
        then
            ignore <| Repository.Clone(url, directory)
        
        let repo = new Repository(directory)
        repository := Some repo

        do repo.Commits
        |> Seq.iter raiseEvent

        // TODO: Start timer and check the repo periodically.

    interface IDisposable with
        member __.Dispose() =
            match !repository with
            | Some(repo) -> repo.Dispose()
            | None -> ()

