namespace Tracktor.Monitor

open Tracktor.Contracts
open FSharp.Desktop.UI

[<AbstractClass>]
type NotificationModel(issue : Issue, commit : Commit) as this =
    inherit Model()

    do this.Issue <- issue
    do this.Commit <- commit

    abstract Issue : Issue with get, set
    abstract Commit : Commit with get, set

    static member create issue commit =
        NotificationModel.Create(issue, commit)
