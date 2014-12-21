namespace Tracktor.Database

open Npgsql
open System
open Tracktor.Service.Common

[<AbstractClass>]
type BaseRepository(configuration : ServiceConfiguration) =
    let connection = new NpgsqlConnection(configuration.ConnectionString)

    let addParameter (command : NpgsqlCommand) name (value : obj) =
        NpgsqlParameter(name, value)
        |> command.Parameters.Add
        |> ignore

    member internal __.CreateCommand sql parameters =
        let command = new NpgsqlCommand(sql, connection)
        Seq.iter (fun p -> addParameter command (fst p) (snd p)) parameters
        command

    interface IDisposable with
        member __.Dispose() = connection.Dispose()
