module Tracktor.Service.Program

open Microsoft.Practices.Unity
open System
open System.ServiceProcess
open Tracktor.Database
open Tracktor.ServiceContracts

let configureServices (container : IUnityContainer) =
    container
        .RegisterType<TracktorWindowsService>()
        .RegisterType<ITracktorService, TracktorService>()

let configure (container : IUnityContainer) =
    container
    |> TracktorDatabase.configure
    |> configureServices

let migrateDatabase (container : IUnityContainer) =
    let updater = container.Resolve<IDatabaseUpdater>()
    updater.Update()

[<EntryPoint>]
let main args =
    use container = configure <| new UnityContainer()
    
    migrateDatabase container

    use service = container.Resolve<TracktorWindowsService>()
    if Environment.UserInteractive
    then 
        service.Start args
        Console.ReadKey() |> ignore
    else ServiceBase.Run service

    0
