module Tracktor.Monitor.MainController

open FSharp.Desktop.UI

let create() =
    let eventHandler event model =
        match event with
        | Exit -> ()

    Controller.Create eventHandler
