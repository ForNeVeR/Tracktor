module Tracktor.Monitor.ApplicationController

open FSharp.Desktop.UI
open System.Windows

let create() =
    let dispatch event (model : ApplicationModel) =
        match event with
        | Exit -> Application.Current.Shutdown(0)

    Controller.Create dispatch
