module Tracktor.Database.Connection

open Npgsql

// TODO: This is a temporary module and it should be here while NpgsqlConnection is not used anywhere else.
// It should be referenced so,ewhere for Npgsql.dd to be copied to the output directory.
let dummy() = 
    let connection = new NpgsqlConnection("")
    ()

