module Tracktor.Monitor.Program

open FSharp.Desktop.UI
open System
open System.Windows

[<EntryPoint; STAThread>]
let main _ = 
    let model = ApplicationModel.Create()
    let view = MainView.create()
    let controller = ApplicationController.create()

    use connection = new TracktorServiceConnection()
    let serviceController = ServiceController.create()

    let mvc = Mvc(model, view, controller).Compose(serviceController, connection)
    use eventLoop = mvc.Start()

    Application().Run view.Root
