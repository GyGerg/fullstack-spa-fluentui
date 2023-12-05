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
        onClick: (unit -> unit) option
        Title: TitleType
    }

    type ToolbarChild =
    | ToolbarButton of ToolbarItem
    | Other of React.Element
        /// First parameter is a function that specifies the second parameter's "position"
    | WrappedToolbarButton of (React.Element -> React.Element) * ToolbarItem 

    [<Inline>]
    let private renderToolbarButton toolbarItem =
        Helpers.Toolbar.button [
            match toolbarItem.Title with
            | Text _ -> ()
            | Icon elt | TextAndIcon (_,elt) -> yield "icon", elt
            match toolbarItem.onClick with
            | Some fn -> yield "onClick", fn
            | None -> ()
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

    let view (props: seq<string*obj>) model = 
        let baseProps = []
        model.children
        |> Seq.map renderToolbarChild
        |> Helpers.toolbar (Seq.append baseProps props)

    