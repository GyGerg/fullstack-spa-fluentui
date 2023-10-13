namespace WsReactExample.Shared
open WebSharper

type IApi = {
    [<Remote>]
    GetValue : unit -> Async<string>
}
