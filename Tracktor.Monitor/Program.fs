module Tracktor.Monitor.Program

open FSharp.Desktop.UI
open System
open System.ServiceModel
open System.Windows
open Tracktor.ServiceContracts

let subscribe() = 
    use factory = new DuplexChannelFactory<ITracktorService>(TracktorServiceCallback(),
                                                             NetTcpBinding(),
                                                             EndpointAddress("net.tcp://localhost:6667"))
    let channel = factory.CreateChannel()

    channel.Subscribe()
    printf "Subscribed"

[<EntryPoint; STAThread>]
let main _ = 
    let model = ApplicationModel.Create()
    let view = MainView.create()
    let controller = ApplicationController.create()

    let mvc = Mvc(model, view, controller)
    use eventLoop = mvc.Start()

    Application().Run( window = view.Root)
