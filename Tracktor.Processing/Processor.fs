namespace Tracktor.Processing

open System
open Tracktor.ServiceContracts

type Processor(callback: ITracktorServiceCallback) =
    let dispatch message = async { return () }
    
    let mailbox = new MailboxProcessor<_>(fun inbox ->
        async { let! message = inbox.Receive()
                return! dispatch message })

    do mailbox.Start()

    member __.Post commit = mailbox.Post <| NewCommit commit
    member __.Post issue = mailbox.Post <| NewIssue issue

    interface IDisposable with
        member __.Dispose() = ()
