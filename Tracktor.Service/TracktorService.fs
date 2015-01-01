namespace Tracktor.Service

open Microsoft.Practices.Unity
open System
open System.ServiceModel
open Tracktor.Contracts
open Tracktor.Service.SourceControl

[<ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)>]
type TracktorService(container : IUnityContainer) =
    let worker: ProjectWorker option ref = ref None
    let contextContainer = container.CreateChildContainer()
    do ignore (contextContainer.RegisterInstance contextContainer)
    
    interface ITracktorService with
        member __.Subscribe() =
            let callback = OperationContext.Current.GetCallbackChannel<ITracktorServiceCallback>()
            contextContainer.RegisterInstance callback |> ignore
            let commitMonitor = contextContainer.Resolve<GitCommitMonitor>()
            contextContainer.RegisterInstance commitMonitor |> ignore
            worker := Some <| contextContainer.Resolve<ProjectWorker>()

    interface IDisposable with
        member __.Dispose() =
            contextContainer.Dispose()
