namespace WsReactExample.Client

open Elmish
open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.Elmish.React
open WebSharper.FluentUI.React.Components
module Icons = WebSharper.FluentUI.React.Icons

[<JavaScript>]
module Client =
    let api = Remote<WsReactExample.Shared.IApi>
    let fetchVal() = 
        async {
            let! hello = api.GetValue()
            printfn $"{hello}"
        }
        |> Async.StartImmediate

    type ReactDom = ReactDOM.ReactDomClient
    type FluentExampleState = {
        count: int
        name: string
    }

    let FunctionComponent (f: 'props -> React.Element) (props: 'props) : React.Element =
        React.CreateElement(f, box props)
        
    let FluentFunctionExample (props:'props) = 
        FunctionComponent (fun props ->
            let cnt, setCnt = React.UseState 0
            FluentUI.React.Helpers.fluentProvider [
                "theme", FluentUI.React.Themes.teamsLightTheme
            ] [
                div [
                    
                    attr.style {|
                        display="flex"
                        flexDirecton="row"
                        margin="15px"
                    |}
                ] [
                    JS.Html $"""
                    <>
                    <{CompoundButton} onClick={fun _ -> setCnt.Invoke(cnt+1)} appearance="primary" icon={{<{Icons.AddRegular} />}}>
                        Increment
                    </{CompoundButton}>
                    <span>Szöveg</span>
                    <span style={{{{fontSize: "2rem"}}}} {{...props}}>{cnt}</span>
                    <{CompoundButton} onClick={fun _ -> setCnt.Invoke(cnt-1)} appearance="brand" icon={{<{Icons.DeleteRegular} />}}>
                        Decrement
                    </{CompoundButton}>
                    </>
                    """
                ]
            ]
        ) props

    type Message = 
    | Increment
    | Decrement

    type Model = {
        asd: int
    }

    let init () = 
        { asd = 0}, Cmd.none
    type Asd = Dispatch<Model>

    let update msg model =
        match msg with
        | Increment -> {model with asd = model.asd+1}, Cmd.none
        | Decrement -> {model with asd = model.asd-1}, Cmd.none

    [<Inline>]
    let buttonInline click icon (text:string) : React.Element =
        JS.Html $"""<{CompoundButton} onClick={click} appearance="primary" icon={{<{icon} />}}>
                {text}
            </{CompoundButton}>"""
    let view model dispatch = 
        FluentUI.React.Helpers.fluentProvider [
            "theme", FluentUI.React.Themes.teamsDarkTheme
        ] [

            buttonInline (fun _ -> dispatch Increment) Icons.AddRegular "Increment"
            text $"%i{model.asd}"
            buttonInline (fun _ -> dispatch Decrement) Icons.SubtractRegular "Decrement"
        ]
        

    [<SPAEntryPoint>]
    let Main () =
        fetchVal()
        
        Program.mkProgram init update view
        |> Program.withReactSynchronous "root"
        |> Program.run
        // let root = ReactDom.CreateRoot(JS.Document.Ge