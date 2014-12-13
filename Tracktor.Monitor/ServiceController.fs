module Tracktor.Monitor.ServiceController

open FSharp.Desktop.UI

let create() =
    let handler event (model : ApplicationModel) =
        match event with
        | FixAvailable(issue, commit) -> printfn "%A -> %A" issue commit

    Controller.Create handler

