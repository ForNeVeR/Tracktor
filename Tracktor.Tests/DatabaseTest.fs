namespace Tracktor.Tests

open Foq
open Microsoft.Practices.Unity
open Microsoft.VisualStudio.TestTools.UnitTesting
open Tracktor.Database
open Tracktor.Processing
open Tracktor.ServiceContracts

[<TestClass>]
type DatabaseTest() =
    let callback = Mock<ITracktorServiceCallback>().Create()
    let processor commitRepository =
        use container = (new UnityContainer()).RegisterInstance<ICommitRepository> commitRepository
        new Processor(container, callback)    

    let commitRepository() =
        Mock<ICommitRepository>()
            .SetupMethod(fun x -> <@ x.Save @>)
            .Returns(async { return () })
            .Create()

    [<TestMethod>]
    member __.PostedCommitShouldBeSaved() =
        let commitRepository = commitRepository()
        let processor = processor commitRepository

        let commit = { Revision = "1"
                       Author = "A" }
        Async.RunSynchronously <| async {
            do! processor.Post commit
        }

        Mock.Verify(<@ commitRepository.Save commit @>, Times.Once)
