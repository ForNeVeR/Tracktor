namespace Tracktor.SourceControl.Svn

open System
open System.Globalization
open SharpSvn
open Tracktor.Contracts

type CommitMonitor() =
    let event = Event<_>()
    let client = new SvnClient()

    let raiseEvent (args: SvnLogEventArgs) =
        let commit = { Revision = args.Revision.ToString(CultureInfo.InvariantCulture)
                       Author = args.Author }
        event.Trigger commit

    member __.NewCommit = event.Publish
    member __.Start() =
        let uri = Uri "https://github.com/miranda-ng/miranda-ng"
        let args = SvnLogArgs(SvnRevisionRange(1L, 100L))
        let log = ref null
        if client.GetLog(uri, args, log)
        then Seq.iter raiseEvent !log
        else ()

    interface IDisposable with
        member __.Dispose() =
            client.Dispose()
