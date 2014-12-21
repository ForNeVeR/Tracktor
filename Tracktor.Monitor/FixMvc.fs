module Tracktor.Monitor.FixMvc

open FSharp.Desktop.UI
open Tracktor.Contracts

let create (model : NotificationModel) =
    let view = FixView.create()
    let controller = FixController.create()
    Mvc(model, view, controller)

