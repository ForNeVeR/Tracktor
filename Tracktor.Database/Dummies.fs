namespace Tracktor.Database

type DummyIssueRepository() =
    interface IIssueRepository with
        member __.Save _ = async { return () }
        member __.Load _ = failwith "Not implemented"

type DummyCommitRepository() =
    interface ICommitRepository with
        member __.Save _ = async { return () }
        member __.Load _ = failwith "Not implemented"
