namespace Tracktor.Service

open System
open Tracktor.ServiceContracts

type ProjectWorker(callback: ITracktorServiceCallback) =
    let issueTracker = new IssueTracker()
    let commitMonitor = new CommitMonitor()

    let raiseCallback (_, commit) = callback.CommitRegistered commit

    do Event.add raiseCallback commitMonitor.NewCommit
    do issueTracker.Start()
    do commitMonitor.Start()

    interface IDisposable with
        member __.Dispose() =
            let disposables: IDisposable list = [issueTracker; commitMonitor]
            disposables |> List.iter (fun d -> d.Dispose())
