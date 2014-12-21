namespace Tracktor.Monitor

open System
open System.ServiceModel
open Tracktor.Contracts

type TracktorServiceConnection() as this =
    let factory = new DuplexChannelFactory<ITracktorService>(this,
                                                             NetTcpBinding(),
                                                             EndpointAddress("net.tcp://localhost:6667"))
    let channel = factory.CreateChannel()
    do channel.Subscribe()

    let fixAvailable = Event<_>()
    
    interface IDisposable with
        member __.Dispose() =
            (factory :> IDisposable).Dispose()

    interface ITracktorServiceCallback with
        member __.FixAvailable(issue, commit) =
            fixAvailable.Trigger(issue, commit)

    interface IObservable<ServiceEvent> with
        member __.Subscribe observer =
            let event = fixAvailable.Publish |> Observable.map FixAvailable
            event.Subscribe observer
