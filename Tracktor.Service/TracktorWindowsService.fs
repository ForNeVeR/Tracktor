namespace Tracktor.Service

open System
open System.ServiceModel
open System.ServiceProcess

type TracktorWindowsService() as this =
    inherit ServiceBase()

    static let serviceName = "TracktorService"
    
    do this.ServiceName <- serviceName
        
    let hostRef: ServiceHost option ref = ref None

    static member Name = serviceName

    override __.OnStart(_ : string[]) =
        let host = new ServiceHost(typeof<TracktorService>, Uri "net.tcp://localhost:6667")
        host.Open()
        hostRef := Some host

    override __.OnStop() =
        match !hostRef with
        | Some(host) -> host.Close()
        | None -> ()

    member x.Start(args : string[]) = x.OnStart args
