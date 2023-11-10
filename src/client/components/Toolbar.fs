namespace WsReactExample.Client.Components

open WebSharper
open WebSharper.JavaScript
open WebSharper.React

open WebSharper.FluentUI.React

[<JavaScript>]
module Toolbar =

    type TitleType =
    | Text of string
    | Icon of React.Element
    | TextAndIcon of string * React.Element
    type ToolbarItem = {
        onClick: unit -> unit
        Title: TitleType
    }

    type ToolbarChild =
    | ToolbarButton of ToolbarItem
    | Other of React.Element
        /// requires a slot thing
    | WrappedToolbarButton of (React.Element -> React.Element) * ToolbarItem 

    [<Inline>]
    let private renderToolbarButton toolbarItem =
        Helpers.Toolbar.button [
            match toolbarItem.Title with
            | Text _ -> ()
            | Icon elt | TextAndIcon (_,elt) -> yield "icon", elt
        ] [
            match toolbarItem.Title with
            | Text txt | TextAndIcon(txt,_) -> Html.text txt
            | _ -> JS.jsx "<></>" // nothing
        ]
    
    [<Inline>]
    let private renderToolbarChild toolbarChild =
        match toolbarChild with
        | ToolbarButton itm -> renderToolbarButton itm
        | Other elt -> elt
        | WrappedToolbarButton(f,itm) -> f (renderToolbarButton itm)

    [<Inline>]
    let private wrapInDialog toolbarItem =
        renderToolbarButton toolbarItem
        |> WsReactExample.Client.Utils.WrapInDialog "" None (Html.div [] [])

    type Model = {
        children: ToolbarChild seq
    }

    let view model = 
        model.children
        |> Seq.map renderToolbarChild
        |> Helpers.toolbar [
            "style", {|
                height="3rem";
                maxHeight= "fit-content";
                overflowY= "hidden";
                overflowX= "auto";
            |}
        ]

    