namespace Tracktor.Service

open Tracktor.Service.Processing
open Tracktor.Service.SourceControl

type ProjectWorker(issueTracker : IssueTracker,
                   commitMonitor : ICommitMonitor,
                   processor : Processor) =
    let processEvent f args = Async.RunSynchronously (f args)

    do issueTracker.NewIssue |> Event.add (processEvent processor.Post)
    do commitMonitor.NewCommit |> Event.add (processEvent processor.Post)

    do issueTracker.Start()
    do commitMonitor.Start()
