namespace Tracktor.Service.SourceControl

open Tracktor.Contracts

type ICommitMonitor =
    abstract NewCommit : IEvent<Commit>
    abstract Start : unit -> unit
