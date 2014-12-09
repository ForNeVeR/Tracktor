namespace Tracktor.Service

open System.ServiceModel
open Tracktor.ServiceContracts

[<ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)>]
type TracktorService() =
    let callback: ITracktorServiceCallback option ref = ref None
    
    interface ITracktorService with
        member __.Subscribe() =
            callback := Some <| OperationContext.Current.GetCallbackChannel<ITracktorServiceCallback>()
            while true do
                System.Threading.Thread.Sleep 5000
                (!callback).Value.CommitRegistered({ Revision = "a"; Author = "x" })
