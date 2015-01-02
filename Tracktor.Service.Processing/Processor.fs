namespace Tracktor.Service.Processing

open Tracktor.Database
open Tracktor.Contracts

type Processor(callback : ITracktorServiceCallback,
               issueRepository : IIssueRepository,
               commitRepository : ICommitRepository) =
    let logErrors action =
        async {
            let! result = Async.Catch action
            match result with
            | Choice1Of2 r -> return r
            | Choice2Of2 ex ->
                printfn "Error: %A" ex // TODO: Proper error logging
                return ()
        }

    let dispatch message = logErrors <| async {
        match message with
        | NewIssue issue ->
            do! issueRepository.Save issue
        | NewCommit commit ->
            do! commitRepository.Save commit
    }

    let mailbox = new MailboxProcessor<_>(fun inbox ->
        async { let! message = inbox.Receive()
                return! dispatch message })

    do mailbox.Start()

    // TODO: On commit of any object check if there are any unresolved fixes in the database.

    let send = mailbox.Post

    member __.Post issue = send <| NewIssue issue
    member __.Post commit = send <| NewCommit commit
