module Tracktor.Service.SourceControl.TracktorServiceSourceControl

open Microsoft.Practices.Unity

let configure (container : IUnityContainer) =
    container.RegisterType<ICommitMonitor, GitCommitMonitor> "git"
