namespace WsReactExample.Client.Pages
open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WebSharper.FluentUI.React.Components
[<JavaScript>]
module ShowcasePage =

    type Model = {
        SelectedTab: string
    }

    type Message = 
    | SelectedTabChanged of string

    let init () =
        {SelectedTab="szoveg1"}, Elmish.Cmd.none
    
    let update msg model =
        match msg with
        | SelectedTabChanged tab ->
            {model with SelectedTab=tab}, Elmish.Cmd.none
    let view model dispatch =
        Html.div [] [
            Helpers.tabList [
                "selectedValue", model.SelectedTab; 
                "onTabSelect", new System.Action<obj,{|value:string|}>(fun a b -> dispatch (SelectedTabChanged b.value))] [
                Helpers.tab ["value", "szoveg1"] [Html.text "Tab 1"]
                Helpers.tab ["value", "szoveg2"] [Html.text "Tab 2"]
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