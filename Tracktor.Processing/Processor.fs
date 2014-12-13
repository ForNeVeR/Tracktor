namespace Tracktor.Processing

open Microsoft.Practices.Unity
open System
open Tracktor.Database
open Tracktor.ServiceContracts

type Processor(container: IUnityContainer, callback: ITracktorServiceCallback) =
    let commitRepository = container.Resolve<ICommitRepository>()

    let dispatch (channel : AsyncReplyChannel<unit>) message = async { 
        match message with
        | NewCommit commit ->
            do! commitRepository.Save commit
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
