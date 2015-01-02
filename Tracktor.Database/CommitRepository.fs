namespace Tracktor.Database

open Tracktor.Service.Common

type CommitRepository(configuration : ServiceConfiguration) =
    inherit BaseRepository(configuration)

    interface ICommitRepository with
        member x.Save commit =
            async {
                use command =
                    x.CreateCommand
                    <| "insert into Commits (IssueKey, Revision, Author) values (:issueKey, :revision, :author)"
                    <| [("issueKey", "")
                        ("revision", commit.Revision)
                        ("author", commit.Author)]
                let! result = Async.AwaitTask <| command.ExecuteNonQueryAsync()
                ignore result
            }
