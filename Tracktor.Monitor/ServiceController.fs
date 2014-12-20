module Tracktor.Monitor.ServiceController

open FSharp.Desktop.UI

let create() =
    let handler event (model : ApplicationModel) =
        match event with
        | FixAvailable(issue, commit) ->
            let notification = NotificationModel.create issue commit
            model.Notifications.Add notification
            
            let mvc = FixMvc.create notification
            Async.StartImmediate <| mvc.StartWindow()

    Controller.Create handler
