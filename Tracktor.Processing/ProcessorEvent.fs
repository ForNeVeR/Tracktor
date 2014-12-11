namespace Tracktor.Processing

open Tracktor.ServiceContracts

type ProcessorEvent =
| NewCommit of Commit
| NewIssue of Issue

