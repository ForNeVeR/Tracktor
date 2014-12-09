module Tracktor.Service.Program

open System.ServiceProcess

[<EntryPoint>]
let main args =
    use service = new Service()
    ServiceBase.Run service
    0
