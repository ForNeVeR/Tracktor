namespace Tracktor.Processing

open Tracktor.Database
open Tracktor.ServiceContracts

type Processor(callback : ITracktorServiceCallback, issueRepository : IIssueRepository, commitRepository : ICommitRepository) =
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
    do ignore <| (Async.StartAsTask <| async {
        // TODO: Remove this. It is here for testing purposes only.
        while true do
            do! Async.Sleep 5000
            let issue = { Id = "id"
                          Name = "name"
                          Assignee = "assignee" }
            let commit = { Revision = "revision"
                           Author = "author" }
            callback.FixAvailable(issue, commit)
    })

    let send msg = mailbox.PostAndAsyncReply (fun channel -> (channel, msg))

    member __.Post issue = send <| NewIssue issue
    member __.Post commit = send <| NewCommit commit
