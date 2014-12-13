namespace Tracktor.ServiceContracts

open System.ServiceModel

[<ServiceContract(Namespace = "http://fornever.me/TracktorService",
                  CallbackContract = typeof<ITracktorServiceCallback>)>]
type ITracktorService =
    [<OperationContract(IsOneWay = true)>]
    abstract Subscribe : unit -> unit
