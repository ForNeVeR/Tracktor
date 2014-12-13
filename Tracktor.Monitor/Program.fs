module Tracktor.Monitor.Program

open FSharp.Desktop.UI
open System
open System.ServiceModel
open Tracktor.ServiceContracts
open System.Windows

let subscribe() = 
    use factory = new DuplexChannelFactory<ITracktorService>(TracktorServiceCallback(),
                                                             NetTcpBinding(),
                                                             EndpointAddress("net.tcp://localhost:6667"))
    let channel = factory.CreateChannel()

    channel.Subscribe()
    printf "Subscribed"

[<EntryPoint; STAThread>]
let main _ = 
    let model = MainWindowModel.Create()
    let view = MainView()
    let controller = MainController.create()

    let mvc = Mvc(model, view, controller)
    use eventLoop = mvc.Start()

    Application().Run( window = view.Root)
