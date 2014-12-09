namespace Tracktor.Service

open System.ServiceModel
open System.ServiceProcess

type Service() as this =
    inherit ServiceBase()

    do this.ServiceName <- "Tracktor"

    let hostRef: ServiceHost ref = ref null

    override __.OnStart(_ : string array) =
        hostRef := new ServiceHost(typeof<TracktorService>)

    override __.OnStop() =
        let host = !hostRef
        if host <> null
        then host.Close()
