namespace Tracktor.Service

open System
open Tracktor.Contracts

type IssueTracker() =
    let event = Event<Issue>()

    member __.NewIssue = event.Publish
    member __.Start() = ()

    interface IDisposable with
        member __.Dispose() = ()
