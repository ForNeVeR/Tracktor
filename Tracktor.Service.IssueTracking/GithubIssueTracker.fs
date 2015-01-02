namespace Tracktor.Service.IssueTracking

open Tracktor.Contracts

type IIssueTracker =
    abstract IssueActivated : IEvent<Issue>
    abstract IssueDeactivated : IEvent<Issue>
    abstract Start : unit -> unit
