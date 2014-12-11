namespace Tracktor.Service

open System
open Tracktor.Processing
open Tracktor.ServiceContracts
open Tracktor.SourceControl.Svn

type ProjectWorker(callback: ITracktorServiceCallback) =
    let issueTracker = new IssueTracker()
    let commitMonitor = new CommitMonitor()

    let processor = new Processor(callback)

    do issueTracker.NewIssue |> Event.add processor.Post
    do commitMonitor.NewCommit |> Event.add processor.Post

    do issueTracker.Start()
    do commitMonitor.Start()

    interface IDisposable with
        member __.Dispose() =
            let disposables: IDisposable list = [issueTracker; commitMonitor]
            disposables |> List.iter (fun d -> d.Dispose())
