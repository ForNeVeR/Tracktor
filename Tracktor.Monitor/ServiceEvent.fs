namespace Tracktor.Monitor

open Tracktor.ServiceContracts

type ServiceEvent = FixAvailable of Issue * Commit
