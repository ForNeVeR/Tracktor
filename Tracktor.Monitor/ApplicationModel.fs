namespace Tracktor.Monitor

open FSharp.Desktop.UI
open System.Collections.ObjectModel

[<AbstractClass>]
type ApplicationModel() =
    inherit Model()

    let notifications = ObservableCollection<NotificationModel>()
    member __.Notifications with get() = notifications
