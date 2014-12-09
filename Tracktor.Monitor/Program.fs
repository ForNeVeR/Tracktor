open System.ServiceModel
open System.ServiceModel.Description
open Tracktor.ServiceContracts

[<EntryPoint>]
let main argv = 
    let endpoint = new ServiceEndpoint(ContractDescription.GetContract typeof<ITracktorService>,
                                       BasicHttpBinding(),
                                       EndpointAddress "http://localhost:6667")
    use factory = new ChannelFactory<ITracktorService>(endpoint)
    let channel = factory.CreateChannel()

    let issues = channel.GetIssues()
    let commits = channel.GetCommits()

    printfn "Got %d issues, %d commits" (Seq.length issues) (Seq.length commits)

    0
