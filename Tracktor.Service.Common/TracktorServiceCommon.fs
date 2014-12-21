module Tracktor.Service.Common.TracktorServiceCommon

open Microsoft.Practices.Unity

let configure (container : IUnityContainer) =
    container.RegisterType<ServiceConfiguration>() 
