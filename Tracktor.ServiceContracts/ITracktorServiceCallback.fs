namespace Tracktor.ServiceContracts

open System.ServiceModel

type ITracktorServiceCallback =
    [<OperationContract(IsOneWay = true)>]
    abstract CommitRegistered: commit: Commit -> unit
