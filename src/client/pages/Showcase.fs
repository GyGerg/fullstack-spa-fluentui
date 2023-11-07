namespace WsReactExample.Client.Pages
open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WebSharper.FluentUI.React.Components
[<JavaScript>]
module ShowcasePage =
    let render () =
        Html.div [] [
            Helpers.tabList [] [
                Helpers.tab [] [Html.text "Tab 1"]
                Helpers.tab [] [Html.text "Tab 2"]
            ]

            Helpers.card [] [
                Helpers.cardHeader [
                    "image", JS.jsx """<img src="https://raw.githubusercontent.com/microsoft/fluentui/master/packages/react-components/react-card/assets/avatar_elvia.svg" />"""
                    "header", JS.jsx """<h4>Sample text</h4>"""
                    "description", Html.text "Amogus xddddd"
                ] [Html.text "Card title"]
                Helpers.cardPreview [] [Html.text "Card Content"]
                Helpers.cardFooter [] [Html.text "Card Footer"]
            ]
            Helpers.field [
                "validationMessage", "Progress Bar"
                "validationState", "none"
            ] [
                Helpers.progressBar ["max",100; "value",58]
            ]
        ]