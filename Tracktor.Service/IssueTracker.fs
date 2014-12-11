namespace Tracktor.Service

open System
open Tracktor.ServiceContracts

type IssueTracker() =
    let event = Event<Issue>()

    member __.NewIssue = event.Publish
    member __.Start() = ()

    interface IDisposable with
        member __.Dispose() = ()
