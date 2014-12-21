namespace Tracktor.Service.Processing

open Tracktor.Contracts

type ProcessorEvent =
| NewCommit of Commit
| NewIssue of Issue
