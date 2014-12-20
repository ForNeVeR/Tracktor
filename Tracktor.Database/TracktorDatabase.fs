module Tracktor.Database.TracktorDatabase

open Microsoft.Practices.Unity

let configure (container : IUnityContainer) =
    container
        .RegisterType<IDatabaseUpdater, DatabaseUpdater>()
        .RegisterType<IIssueRepository, DummyIssueRepository>() // TODO: Implement the real repository.
        .RegisterType<ICommitRepository, DummyCommitRepository>() // TODO: Implement the real repository.
