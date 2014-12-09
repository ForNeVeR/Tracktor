namespace Tracktor.ServiceContracts

open System.ServiceModel

[<ServiceContract(Namespace = "http://fornever.me/TracktorService")>]
type ITracktorService =
    [<OperationContract>]
    abstract member GetIssues : unit -> Issue seq

    [<OperationContract>]
    abstract member GetCommits : unit -> Issue seq

