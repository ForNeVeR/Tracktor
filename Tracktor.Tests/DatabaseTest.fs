namespace Tracktor.Tests

open Foq
open Microsoft.VisualStudio.TestTools.UnitTesting
open Tracktor.Database
open Tracktor.Processing
open Tracktor.ServiceContracts

[<TestClass>]
type DatabaseTest() =
    let callback = Mock<ITracktorServiceCallback>().Create()
    let processor = new Processor(callback)

    let mockCommitRepository() =
        Mock<ICommitRepository>()
            .SetupMethod(fun x -> <@ x.Save @>)
            .Returns(async { return 1L })
            .Create()

    [<TestMethod>]
    member __.PostedCommitShouldBeSaved() =
        let commitRepository = mockCommitRepository()            
        // TODO: Create dependency container and put the repository there

        let commit = { Revision = "1"
                       Author = "A" }
        Async.RunSynchronously <| async {
            do! processor.Post commit
        }

        Mock.Verify(<@ commitRepository.Save commit @>, Times.Once)
