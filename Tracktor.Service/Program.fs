module Tracktor.Service.Program

open System
open System.ServiceProcess

[<EntryPoint>]
let main args =
    use service = new TracktorWindowsService()
    if Environment.UserInteractive
    then 
        service.Start args
        Console.ReadKey() |> ignore
    else ServiceBase.Run service

    0
