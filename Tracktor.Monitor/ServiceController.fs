module Tracktor.Monitor.ServiceController

open FSharp.Desktop.UI

let create() =
    let handler event (model : ApplicationModel) =
        match event with
        | FixAvailable(issue, commit) ->
            let notification = NotificationModel.Create()
            model.Notifications.Add notification

    Controller.Create handler

