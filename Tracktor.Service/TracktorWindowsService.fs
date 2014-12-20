namespace Tracktor.Service

open Microsoft.Practices.Unity
open System
open System.ServiceModel
open System.ServiceProcess
open Unity.Wcf
open Tracktor.ServiceContracts

type TracktorWindowsService(container : IUnityContainer) as this =
    inherit ServiceBase()

    static let serviceName = "TracktorService"
    
    do this.ServiceName <- serviceName
        
    let hostRef: ServiceHost option ref = ref None

    static member Name = serviceName

    override __.OnStart(_ : string[]) =
        let serviceType = 
            container.Registrations
            |> Seq.filter (fun r -> r.RegisteredType = typeof<ITracktorService>)
            |> Seq.map (fun r -> r.MappedToType)
            |> Seq.head
        let host = new UnityServiceHost(container, serviceType, Uri "net.tcp://localhost:6667")
        host.Open()
        hostRef := Some <| upcast host

    override __.OnStop() =
        match !hostRef with
        | Some(host) -> host.Close()
        | None -> ()

    member x.Start(args : string[]) = x.OnStart args
