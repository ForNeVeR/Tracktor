namespace Tracktor.Service

open System
open System.ServiceModel
open Tracktor.ServiceContracts

[<ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)>]
type TracktorService() =
    let worker: ProjectWorker option ref = ref None
    
    interface ITracktorService with
        member __.Subscribe() =
            let callback = OperationContext.Current.GetCallbackChannel<ITracktorServiceCallback>()
            worker := Some <| new ProjectWorker(callback)

    interface IDisposable with
        member __.Dispose() =
            match !worker with
            | Some w -> (w :> IDisposable).Dispose()
            | None -> ()

