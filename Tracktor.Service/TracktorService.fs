namespace Tracktor.Service

open System.ServiceModel
open Tracktor.ServiceContracts

[<ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)>]
type TracktorService() =
    interface ITracktorService with
        member __.GetIssues() = Seq.empty
        member __.GetCommits() = Seq.empty
