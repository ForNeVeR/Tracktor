module Tracktor.Monitor.FixMvc

open FSharp.Desktop.UI
open Tracktor.ServiceContracts

let create(issue : Issue, commit : Commit) =
    let model = FixModel.create issue commit
    let view = FixView.create()
    let controller = FixController.create()
    Mvc(model, view, controller)

