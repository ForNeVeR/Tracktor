namespace Tracktor.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Tracktor.Database
open Tracktor.Processing
open Tracktor.ServiceContracts

[<TestClass>]
type DatabaseTest() =
    let commitRepository : ICommitRepository = failwith "Not implemented" // TODO: Mock ICommitRepository
    // TODO: Create dependency container and put the repository there
    let callback : ITracktorServiceCallback = failwith "Not implemented" // TODO: Mock ITracktorServiceCallback
    let processor = new Processor(callback)    

    [<TestMethod>]
    member __.PostedCommitShouldBeSaved() =
        let commit = { Revision = "1"
                       Author = "A" }
        Async.RunSynchronously <| async {
            do! processor.Post commit
            failwith "Not finished" // TODO: Check whether mocked Save method was called
        }
