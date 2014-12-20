module Tracktor.Monitor.FixController

open FSharp.Desktop.UI

let create() =
    let dispatch _ _ = ()

    Controller.Create dispatch
