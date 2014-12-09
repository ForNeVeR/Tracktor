namespace Tracktor.Monitor

open Tracktor.ServiceContracts

type TracktorServiceCallback() =
    interface ITracktorServiceCallback with
        member __.CommitRegistered commit =
            printfn "%A" commit
