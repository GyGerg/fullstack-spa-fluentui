namespace WsReactExample.Client
open WebSharper
open WebSharper.JavaScript
open WebSharper.Remoting

[<JavaScript>]
module ServerCommunication =
    let api = Remote<WsReactExample.Shared.IApi>
    let fetchVal() = 
        async {
            let! hello = api.GetValue()
            printfn $"{hello}"
        }
        |> Async.StartImmediate