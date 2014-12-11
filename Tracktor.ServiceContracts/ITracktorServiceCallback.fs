namespace Tracktor.ServiceContracts

open System.ServiceModel

type ITracktorServiceCallback =
    [<OperationContract(IsOneWay = true)>]
    abstract FixAvailable : fix: Issue * Commit -> unit
