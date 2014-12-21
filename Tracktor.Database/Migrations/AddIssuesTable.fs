namespace Tracktor.Database.Migrations

open FluentMigrator

[<Migration(2L)>]
type AddIssuesTable() =
    inherit Migration()

    override x.Up() =
        x.Create.Table("Issues")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Key").AsString().Nullable().Indexed()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Assignee").AsString().NotNullable()
        |> ignore

    override x.Down() =
        x.Delete.Table("Issues")
        |> ignore
