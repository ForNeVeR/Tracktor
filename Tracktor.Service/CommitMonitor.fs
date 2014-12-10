namespace Tracktor.Service

open System
open Tracktor.ServiceContracts

type CommitMonitor() as this =
    let event = Event<_>()

    member __.NewCommit = event.Publish
    member __.Start() = event.Trigger(this, { Revision = "id"; Author = "author" })

    interface IDisposable with
        member __.Dispose() = ()
