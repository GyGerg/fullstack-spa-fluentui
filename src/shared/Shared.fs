﻿namespace WsReactExample.Shared
open WebSharper

[<JavaScript>]
type FundraiserInfoServer = {
    Name:string
    FundraiserLink: JavaScript.URL option
    Owner: {|
        Name:string
    |}
    OwnerLink: JavaScript.URL option
    Goal: float
}
type IApi = {
    [<Remote>] GetValue : unit -> Async<string>

    [<Remote>] GetFundraisers: unit -> Async<FundraiserInfoServer array>
}
