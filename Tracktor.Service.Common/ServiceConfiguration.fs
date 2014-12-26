namespace Tracktor.Service.Common

open System.Configuration
open System.IO

type ServiceConfiguration() =
    member __.ConnectionString with get() =
        ConfigurationManager.ConnectionStrings.["Tracktor"].ConnectionString

    member __.DataDirectory with get() =
        Path.GetTempPath()
