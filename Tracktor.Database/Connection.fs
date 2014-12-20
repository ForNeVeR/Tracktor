module Tracktor.Database.Connection

open Npgsql

// TODO: This is a temporary module and it should be here while NpgsqlConnection is not used anywhere else.
// It should be referenced somewhere for the Npgsql.dll to be copied to the output directory.
let dummy() = 
    let connection = new NpgsqlConnection("")
    ()

