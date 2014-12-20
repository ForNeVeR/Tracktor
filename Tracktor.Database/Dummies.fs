namespace Tracktor.Database

type DummyIssueRepository() =
    interface IIssueRepository with
        member __.Save _ = async { return () }

type DummyCommitRepository() =
    interface ICommitRepository with
        member __.Save _ = async { return () }
