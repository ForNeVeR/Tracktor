module Tracktor.Service.Program

open Microsoft.Practices.Unity
open System
open System.ServiceProcess
open Tracktor.Database
open Tracktor.ServiceContracts

let configure (container : IUnityContainer) =
    container
        .RegisterType<TracktorWindowsService>()
        .RegisterType<ITracktorService, TracktorService>()
        .RegisterType<IIssueRepository, DummyIssueRepository>() // TODO: Implement the real repository.
        .RegisterType<ICommitRepository, DummyCommitRepository>() // TODO: Implement the real repository.

[<EntryPoint>]
let main args =
    use container = configure <| new UnityContainer()
    use service = container.Resolve<TracktorWindowsService>()
    if Environment.UserInteractive
    then 
        service.Start args
        Console.ReadKey() |> ignore
    else ServiceBase.Run service

    0
