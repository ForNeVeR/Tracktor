namespace Tracktor.Database

open FluentMigrator
open FluentMigrator.Runner
open FluentMigrator.Runner.Announcers
open FluentMigrator.Runner.Initialization
open FluentMigrator.Runner.Processors.Postgres
open System
open System.Reflection
open Tracktor.Service.Common

type DatabaseUpdater(configuration : ServiceConfiguration) =
    member __.Update() =
        let assembly = Assembly.GetExecutingAssembly()
        let announcer = TextWriterAnnouncer(Console.Out)
        let context = RunnerContext(announcer, Namespace = "Tracktor.Database.Migrations")
        let connectionString = configuration.ConnectionString
        let options =
            {
                new IMigrationProcessorOptions with
                    member __.PreviewOnly with get() = false
                    member __.ProviderSwitches with get() = null
                    member __.Timeout with get() = 60 
            }
        let factory = PostgresProcessorFactory()
        let processor = factory.Create(connectionString, announcer, options)
        let runner = new MigrationRunner(assembly, context, processor)
        runner.MigrateUp()
