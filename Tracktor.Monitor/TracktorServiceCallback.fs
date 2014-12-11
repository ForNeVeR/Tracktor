namespace Tracktor.Monitor

open Tracktor.ServiceContracts

type TracktorServiceCallback() =
    interface ITracktorServiceCallback with
        member __.FixAvailable (issue, commit) =
            printfn "%A -> %A" issue commit
