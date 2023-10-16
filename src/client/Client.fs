namespace WsReactExample.Client

open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React
open FluentUi.Bindings
// open FluentUi.Components

[<JavaScript>]
module Client =
    let api = Remote<WsReactExample.Shared.IApi>
    let fetchVal() = 
        async {
            let! hello = api.GetValue()
            printfn $"{hello}"
        }
        |> Async.StartImmediate

    type ReactDom = ReactDOM.Bindings.ReactDomClient
    type FluentExampleState = {
        count: int
        name: string
    }

    type FluentExampleProps = unit
    type FluentExample() as this =
        inherit React.Component<FluentExampleProps,FluentExampleState>()
        do
            this.SetInitialState {count = 0; name = "Nevenincs"}


        override this.Render() =
        
            let addBtn  = 
                compoundButtonInline 
                    "Add" 
                    "increment" 
                    // (JS.Html $"""{{<{FluentUi.Icons.addRegular} />}}""") 
                    (React.CreateElement(FluentUi.Icons.AddRegular, {||}))
                    (fun e -> 
                        this.SetState {this.State with count = (this.State.count + 1); name = "Nev"}
                        printfn $"{this.State.count}"
                        )
            let decrementBtn = 
                compoundButtonInline 
                    "Subtract" 
                    "decrement"  
                    (React.CreateElement(``type``=FluentUi.Icons.DeleteRegular<string>, props={||}))
                    (fun e -> 
                        this.SetState({this.State with count = (this.State.count - 1)}, fun _ -> printfn $"{this.State.count}")
                    )

            fluentProvider [
                "theme", FluentUi.Themes.teamsLightTheme
            ] [|
                    div [
                        attr.style {|display="flex"; margin="5px"; flexDirection="row";|}
                        
                        
                    ] [
                        addBtn
                        span [attr.style {|fontSize = "2rem"|}] [text $"{this.State.count}"]
                        decrementBtn
                    ]
                |]

    let FunctionComponent (f: 'props -> React.Element) (props: 'props) : React.Element =
        React.CreateElement(f, box props)

    let FluentFunctionExample() = 
        FunctionComponent (fun props ->
            let cnt, setCnt = React.UseState 0
            FluentUi.Components.fluentProvider [
                "theme", FluentUi.Themes.teamsLightTheme
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
                    <{CompoundButton} onClick={fun _ -> setCnt.Invoke(cnt+1)} appearance="primary" icon={{<{FluentUi.Icons.AddRegular} />}}>
                        Increment
                    </{CompoundButton}>
                    <span style={ {|fontSize= "2rem"|} }>{cnt}</span>
                    <{CompoundButton} onClick={fun _ -> setCnt.Invoke(cnt-1)} appearance="brand" icon={{<{FluentUi.Icons.DeleteRegular} />}}>
                        Decrement
                    </{CompoundButton}>
                    </>
                    """
                ]
            ]
        ) []

    [<SPAEntryPoint>]
    let Main () =
        fetchVal()
        // let root = createRoot (JS.Document.GetElementById "root")
        let root = ReactDom.CreateRoot(JS.Document.GetElementById "root")
        root.Render(FluentFunctionExample())