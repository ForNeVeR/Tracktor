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
    let processor issueRepository commitRepository =
        use container = 
            (new UnityContainer())
                .RegisterInstance<IIssueRepository>(issueRepository)
                .RegisterInstance<ICommitRepository>(commitRepository)                
        new Processor(container, callback)

    let issueRepository() =
        Mock<IIssueRepository>()
            .SetupMethod(fun x -> <@ x.Save @>)
            .Returns(async { return () })
            .Create()

    let commitRepository() =
        Mock<ICommitRepository>()
            .SetupMethod(fun x -> <@ x.Save @>)
            .Returns(async { return () })
            .Create()

    [<TestMethod>]
    member __.PostedCommitShouldBeSaved() =
        let issueRepository = issueRepository()
        let commitRepository = commitRepository()
        let processor = processor issueRepository commitRepository

        let commit = { Revision = "1"
                       Author = "A" }
        Async.RunSynchronously <| async {
            do! processor.Post commit
        }

        Mock.Verify(<@ commitRepository.Save commit @>, Times.Once)

    [<TestMethod>]
    member __.PostedIssueShouldBeSaved() =
        let issueRepository = issueRepository()
        let commitRepository = commitRepository()
        let processor = processor issueRepository commitRepository

        let issue = { Id = "1"
                      Name = "N"
                      Assignee = "A" }
        Async.RunSynchronously <| async {
            do! processor.Post issue
        }

        Mock.Verify(<@ issueRepository.Save issue @>, Times.Once)
