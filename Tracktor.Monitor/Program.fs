module Tracktor.Monitor.Program

open System
open System.ServiceModel
open Tracktor.ServiceContracts

[<EntryPoint>]
let main argv = 
    use factory = new DuplexChannelFactory<ITracktorService>(TracktorServiceCallback(),
                                                             NetTcpBinding(),
                                                             EndpointAddress("net.tcp://localhost:6667"))
    let channel = factory.CreateChannel()

    channel.Subscribe()
    printf "Subscribed"

    Console.ReadKey() |> ignore

    0
