module Tracktor.Service.Program

open Microsoft.Practices.Unity
open System
open System.ServiceProcess
open Tracktor.Contracts
open Tracktor.Database
open Tracktor.Service.Common
open Tracktor.Service.SourceControl

let configureServices (container : IUnityContainer) =
    container
        .RegisterType<TracktorWindowsService>()
        .RegisterType<ITracktorService, TracktorService>()

let configure (container : IUnityContainer) =
    container
    |> TracktorServiceCommon.configure
    |> TracktorDatabase.configure
    |> TracktorServiceSourceControl.configure
    |> configureServices

let migrateDatabase (container : IUnityContainer) =
    let updater = container.Resolve<DatabaseUpdater>()
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
