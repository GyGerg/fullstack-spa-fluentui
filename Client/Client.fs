namespace WsReactExample.Client

open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React

[<JavaScript>]
module Client =

    let [<Literal>] fluentUi = "@fluentui/react-components"
    
    [<Import(fluentUi)>]
    module FluentUi =
        module Themes =
            let teamsLightTheme = JS.Import<obj>("teamsLightTheme", fluentUi)
            let teamsDarkTheme = JS.Import<obj>("teamsDarkTheme", fluentUi)
        // [<Name "CompoundButton">] 
        let inline compoundImport (o:obj) :  React.Component<obj,obj> = JS.Import<obj -> React.Component<obj,obj>>("CompoundButton",fluentUi) o
        let compoundButton (props: obj) ([<System.ParamArray>] children: React.Element array) = React.CreateElement(compoundImport, props, children)
        
        let compoundButtonInline (text:string) (secondaryText:string) (onClick: Bindings.MouseEvent -> unit) : React.Component<_,_> = JS.Html $"""
            <{compoundImport} secondaryContent={secondaryText} onClick={onClick} appearance="primary">
                {text}
            </{compoundImport}>
        """
        // [<Name "FluentProvider">] 
        let fluentProvider (props: obj) ([<System.ParamArray>] children: React.Element array) = React.CreateElement(JS.Import("FluentProvider",fluentUi), props, children)

    // built-in React.Mount threw some errors around
    type ReactRoot =
        [<Name "render">] 
        member inline _.Render(element:React.Element) : unit = JS.Import("render", "react-dom/client") element

    let inline createRoot (element:Dom.Element) : ReactRoot = JS.Import("createRoot", "react-dom/client") element

    

    type FluentExample() =
        inherit React.Component<unit,unit>()
        override this.Render() =
            FluentUi.fluentProvider {|
                theme = FluentUi.Themes.teamsLightTheme
            |} [|
                    FluentUi.compoundButton {|
                        appearance = "primary"
                        onClick = (fun (x:React.Bindings.MouseEvent) -> ())
                        ``aria-label`` = "ariaLabelAttempt"
                        secondaryContent = "SecondaryText"
                    |} [|div [] [text "szoveg"] |]

                    FluentUi.compoundButtonInline  "Button2" "jsx inline" (fun e -> Console.Log "Inline btn clicked")
                    |> As<React.Element>
                    
                |]

    [<SPAEntryPoint>]
    let Main () =
        Console.Log "test log"
        let root = createRoot (JS.Document.GetElementById "root")
        root.Render(React.Make FluentExample ())

        // React.Make FluentExample ()
        // |> React.Mount (JS.Document.GetElementById "root")