namespace Tracktor.Service

open Tracktor.ServiceContracts

type TracktorService() =
    interface ITracktorService with
        member __.GetIssues() = Seq.empty
        member __.GetCommits() = Seq.empty
