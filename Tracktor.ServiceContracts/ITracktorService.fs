namespace Tracktor.ServiceContracts

open System.ServiceModel

[<ServiceContract(Namespace = "http://Tracktor.ServiceContract.TracktorService")>]
type ITracktorService =
    [<OperationContract>]
    abstract member GetIssues : unit -> Issue seq

    [<OperationContract>]
    abstract member GetCommits : unit -> Issue seq

