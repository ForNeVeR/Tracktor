namespace Tracktor.Database

open Tracktor.Service.Common

type IssueRepository(configuration : ServiceConfiguration) =
    inherit BaseRepository(configuration)

    interface IIssueRepository with
        member x.Save issue =
            async {
                use command =
                    x.CreateCommand 
                    <| "insert into Issues (Key, Name, Assignee) values (:key, :name, :assignee)"
                    <| [("key", issue.Key)
                        ("name", issue.Name)
                        ("assignee", issue.Assignee)]
                let! result = Async.AwaitTask <| command.ExecuteNonQueryAsync()
                ignore result
            }
