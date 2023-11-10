namespace WsReactExample.Client

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React

[<JavaScript>]
module Utils =
    let isMobile() = JS.Window.MatchMedia("all and (max-width: 600px)").Matches

    let resizeSub onResize =
        let start dispatch =
            let evt = new System.Action<Dom.Event>(fun _ -> dispatch (onResize <| isMobile()))
            JS.Window.AddEventListener("resize", evt)
            {new System.IDisposable with 
                member _.Dispose() = JS.Window.RemoveEventListener("resize", evt)}
        start
    
    let [<Inline>] WrapInDialog (className:string) (mountNode:obj option) (dialogContent:React.Element) trigger =
        Helpers.dialog [
            "modalType", box "modal"
            if mountNode.IsSome then ("mountNode", mountNode.Value)
        ] [
            Helpers.Dialog.trigger [] [trigger]
            Helpers.Dialog.surface [
                "className", className
                "mountNode", {|className= "dialogContainer"|}
            ] [
                ReactHelpers.Elt DialogContent [] [
                    Helpers.Dialog.title [] [
                        // Html.h3 [] [Html.text "Settings"]
                    ]
                    ReactHelpers.Elt DialogContent [
                        "style", {|
                            align="right"
                        |}
                    ] [
                        dialogContent
                    ]
                    Helpers.Dialog.actions [
                        "fluid", true
                    ] [
                        // Helpers.button ["appearance", "secondary"] [Html.text "Close"]
                    ]
                ]
            ]
                        
        ]