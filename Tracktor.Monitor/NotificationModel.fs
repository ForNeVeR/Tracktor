namespace Tracktor.Monitor

open Tracktor.ServiceContracts
open FSharp.Desktop.UI

[<AbstractClass>]
type NotificationModel() =
    inherit Model()

    abstract Issue : Issue with get, set
    abstract Commit : Commit with get, set
