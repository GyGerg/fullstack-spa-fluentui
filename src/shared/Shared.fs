namespace WsReactExample.Shared
open WebSharper

type IApi = {
    [<Remote>] GetValue : unit -> Async<string>

    [<Remote>] GetFundraisers: unit -> Async<FundraiserInfoServer array>
}
