namespace Tracktor.Processing

open System
open Tracktor.ServiceContracts

type Processor(callback: ITracktorServiceCallback) =
    let dispatch message = async { return () }
    
    let mailbox = new MailboxProcessor<_>(fun inbox ->
        async { let! message = inbox.Receive()
                return! dispatch message })

    do mailbox.Start()

    let send msg : unit Async = mailbox.PostAndAsyncReply (fun _ -> msg)

    member __.Post commit = send <| NewCommit commit
    member __.Post issue = send <| NewIssue issue

    interface IDisposable with
        member __.Dispose() = ()
