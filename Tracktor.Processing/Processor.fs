namespace Tracktor.Processing

open System
open Tracktor.ServiceContracts

type Processor(callback: ITracktorServiceCallback) =
    let dispatch (channel : AsyncReplyChannel<unit>) message = async { 
        channel.Reply ()
    }
    
    let mailbox = new MailboxProcessor<_>(fun inbox ->
        async { let! (channel, message) = inbox.Receive()
                return! dispatch channel message })

    do mailbox.Start()

    let send msg = mailbox.PostAndAsyncReply (fun channel -> (channel, msg))

    member __.Post commit = send <| NewCommit commit
    member __.Post issue = send <| NewIssue issue

    interface IDisposable with
        member __.Dispose() = ()
