module Tracktor.Database.TracktorDatabase

open Microsoft.Practices.Unity

let configure (container : IUnityContainer) =
    container
        .RegisterType<DatabaseUpdater>()
        .RegisterType<IIssueRepository, IssueRepository>()
        .RegisterType<ICommitRepository, CommitRepository>()
