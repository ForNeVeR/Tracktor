namespace Tracktor.Service

open System

type IssueTracker() =
    let event = Event<_>()

    member __.NewIssue = event.Publish
    member __.Start() = ()

    interface IDisposable with
        member __.Dispose() = ()
