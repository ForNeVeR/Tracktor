namespace Tracktor.Database.Migrations

open FluentMigrator

[<Migration(1L)>]
type AddCommitsTable() =
    inherit Migration()

    override x.Up() =
        x.Create.Table("Commits")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("IssueKey").AsString().Nullable().Indexed()
            .WithColumn("Revision").AsString().NotNullable()
            .WithColumn("Author").AsString().NotNullable()
        |> ignore

    override x.Down() =
        x.Delete.Table("Commits")
        |> ignore
