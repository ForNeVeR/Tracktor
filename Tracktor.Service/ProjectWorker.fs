namespace Tracktor.Service

open System
open Tracktor.Processing
open Tracktor.ServiceContracts
open Tracktor.SourceControl.Svn

type ProjectWorker(callback: ITracktorServiceCallback) =
    let issueTracker = new IssueTracker()
    let commitMonitor = new CommitMonitor()

    let processor = new Processor(callback)

    let processEvent f args = Async.RunSynchronously (f args)

    do issueTracker.NewIssue |> Event.add (processEvent processor.Post)
    do commitMonitor.NewCommit |> Event.add (processEvent processor.Post)

    do issueTracker.Start()
    do commitMonitor.Start()

    interface IDisposable with
        member __.Dispose() =
            let disposables: IDisposable list = [issueTracker; commitMonitor]
            disposables |> List.iter (fun d -> d.Dispose())
