namespace Tracktor.Service.Common

open System.Configuration

type ServiceConfiguration() =
    member __.ConnectionString with get() =
        ConfigurationManager.ConnectionStrings.["Tracktor"].ConnectionString
