namespace Tracktor.Processing

open Microsoft.Practices.Unity
open System
open Tracktor.Database
open Tracktor.ServiceContracts

type Processor(container: IUnityContainer, callback: ITracktorServiceCallback) =
    let issueRepository = container.Resolve<IIssueRepository>()
    let commitRepository = container.Resolve<ICommitRepository>()

    let dispatch (channel : AsyncReplyChannel<unit>) message = async { 
        match message with
        | NewIssue issue ->
            do! issueRepository.Save issue
            channel.Reply ()
        | NewCommit commit ->
            do! commitRepository.Save commit
            channel.Reply ()
    }
    
    let mailbox = new MailboxProcessor<_>(fun inbox ->
        async { let! (channel, message) = inbox.Receive()
                return! dispatch channel message })

    do mailbox.Start()

    let send msg = mailbox.PostAndAsyncReply (fun channel -> (channel, msg))

    member __.Post issue = send <| NewIssue issue
    member __.Post commit = send <| NewCommit commit

    interface IDisposable with
        member __.Dispose() = ()
