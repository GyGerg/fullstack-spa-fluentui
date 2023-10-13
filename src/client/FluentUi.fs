namespace WsReactExample.Client

open WebSharper.JavaScript
open WebSharper.React
open WebSharper
[<JavaScript;AutoOpen; Sealed>]
module private Constants =
    let [<Literal>] fluentUi = "@fluentui"
    let [<Literal>] fluentComponents = fluentUi+"/react-components"
    let [<Literal>] fluentIcons = fluentUi+"/react-icons"

[<JavaScript;Import(fluentComponents);Sealed>]
module FluentUi =
    module Themes =
        let teamsLightTheme = JS.Import<obj>("teamsLightTheme", fluentComponents)
        let teamsDarkTheme = JS.Import<obj>("teamsDarkTheme", fluentComponents)

    module Icons =
        [<Inline; Name "AddRegular">] 
        let addRegular = JS.Import<obj>("AddRegular", fluentIcons)
        [<Inline; Name "DeleteRegular">] 
        let deleteRegular = JS.Import<obj>("DeleteRegular", fluentIcons)
    // [<Name "CompoundButton">] 

    [<Inline>] 
    let CompoundButton = JS.Import<obj>("CompoundButton",fluentComponents)
    
    let compoundButtonFunc (props: obj) ([<System.ParamArray>] children: React.Element array) = React.CreateElement(CompoundButton, props, children)
    
    let compoundButtonInline (text:string) (secondaryText:string) (icon:obj) (onClick: Bindings.MouseEvent -> unit) : React.Element = JS.Html $"""
        <{CompoundButton} secondaryContent={secondaryText} onClick={onClick} appearance="primary" icon={icon} >
            {text}
        </{CompoundButton}>
    """
    // [<Name "FluentProvider">] 
    let inline fluentProvider (props: obj) ([<System.ParamArray>] children: React.Element seq) = 
        React.CreateElement(JS.Import("FluentProvider",fluentComponents), props, Array.ofSeq children)
