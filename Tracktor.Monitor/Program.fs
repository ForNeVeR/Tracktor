module Tracktor.Monitor.Program

open System
open System.Windows

[<EntryPoint; STAThread>]
let main _ = 
    use connection = new TracktorServiceConnection()
    let (mvc, window) = ApplicationMvc.create connection
    use eventLoop = mvc.Start()

    Application().Run window
