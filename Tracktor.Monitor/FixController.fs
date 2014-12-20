module Tracktor.Monitor.FixController

open FSharp.Desktop.UI

let create() =
    let dispatch event (model : FixModel) =
        ()

    Controller.Create dispatch
