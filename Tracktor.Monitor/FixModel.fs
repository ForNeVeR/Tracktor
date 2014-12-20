namespace Tracktor.Monitor

open FSharp.Desktop.UI
open Tracktor.ServiceContracts

[<AbstractClass>]
type FixModel(issue : Issue, commit : Commit) as this =
    inherit Model()

    do this.Issue <- issue
    do this.Commit <- commit

    abstract Issue : Issue with get, set
    abstract Commit : Commit with get, set

    static member create issue commit =
        FixModel.Create(issue, commit)
