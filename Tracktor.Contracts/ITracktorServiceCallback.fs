﻿namespace Tracktor.Contracts

open System.ServiceModel

type ITracktorServiceCallback =
    [<OperationContract(IsOneWay = true)>]
    abstract FixAvailable : issue : Issue * commit : Commit -> unit
