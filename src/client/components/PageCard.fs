namespace WsReactExample.Client

open WebSharper
open WebSharper.React
open WebSharper.Elmish.React
open WebSharper.FluentUI.React

[<JavaScript>]
module PageCard =
    let render (title:string) (child:React.Element) =
        Helpers.card [
                "style", {|rowGap="50px";|}
            ] [
                Helpers.cardHeader [] [Html.h3 [] [Html.text title]]
                Helpers.cardPreview [] [
                    child
                ]
                Helpers.cardFooter [] [
                    Html.text "invisible footer text"
                ]
            ]