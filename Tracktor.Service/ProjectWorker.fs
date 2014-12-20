namespace Tracktor.Service

open Microsoft.Practices.Unity
open Tracktor.Processing
open Tracktor.SourceControl.Svn

type ProjectWorker(container : IUnityContainer) =
    let issueTracker = container.Resolve<IssueTracker>()
    let commitMonitor = container.Resolve<CommitMonitor>()

    do ignore(container.RegisterInstance(issueTracker).RegisterInstance(commitMonitor))

    let processor = container.Resolve<Processor>()

    let processEvent f args = Async.RunSynchronously (f args)

    do issueTracker.NewIssue |> Event.add (processEvent processor.Post)
    do commitMonitor.NewCommit |> Event.add (processEvent processor.Post)

    do issueTracker.Start()
    do commitMonitor.Start()
