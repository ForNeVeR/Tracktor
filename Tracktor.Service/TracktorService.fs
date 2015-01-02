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

    interface ITracktorService with
        member __.Subscribe parameters =
            contextContainer.RegisterInstance parameters |> ignore

            let callback = OperationContext.Current.GetCallbackChannel<ITracktorServiceCallback>()
            let commitMonitor = contextContainer.Resolve<ICommitMonitor> "git"

            contextContainer
                .RegisterInstance(callback)
                .RegisterInstance(commitMonitor)
            |> ignore

            worker := Some <| contextContainer.Resolve<ProjectWorker>()

    interface IDisposable with
        member __.Dispose() =
            contextContainer.Dispose()
