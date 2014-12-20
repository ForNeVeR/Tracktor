module Tracktor.Monitor.ApplicationMvc

open FSharp.Desktop.UI
open System.Windows

let create(serviceConnection : TracktorServiceConnection) : Mvc<_, _> * Window =
    let model = ApplicationModel.Create()
    let view = MainView.create()
    let controller = ApplicationController.create()

    let serviceController = ServiceController.create()
    let mvc = Mvc(model, view, controller).Compose(serviceController, serviceConnection)
    (mvc, view.Root)

