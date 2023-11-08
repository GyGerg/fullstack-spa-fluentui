namespace WsReactExample.Client

open WebSharper
open WebSharper.JavaScript
[<JavaScript>]
module Utils =
    let isMobile() = not <| JS.Window.MatchMedia("all and (min-width: 768px)").Matches

    let resizeSub onResize =
        let start dispatch =
            let evt = new System.Action<Dom.Event>(fun _ -> dispatch (onResize <| JS.Window.MatchMedia("").Matches))
            JS.Window.AddEventListener("resize", evt)
            {new System.IDisposable with 
                member _.Dispose() = JS.Window.RemoveEventListener("resize", evt)}
        start
    