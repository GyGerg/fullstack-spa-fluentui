namespace WsReactExample.Client

open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React

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
                FluentUi.compoundButtonInline 
                    "Add" 
                    "increment" 
                    // (JS.Html $"""{{<{FluentUi.Icons.addRegular} />}}""") 
                    (React.CreateElement(FluentUi.Icons.addRegular, {||}))
                    (fun e -> 
                        this.SetState {this.State with count = (this.State.count + 1); name = "Nev"}
                        printfn $"{this.State.count}"
                        )
            let decrementBtn = 
                FluentUi.compoundButtonInline 
                    "Subtract" 
                    "decrement"  
                    (React.CreateElement(FluentUi.Icons.deleteRegular, {||}))
                    (fun e -> 
                        this.SetState({this.State with count = (this.State.count - 1)}, fun _ -> printfn $"{this.State.count}")
                    )

            FluentUi.fluentProvider {|
                theme = FluentUi.Themes.teamsLightTheme
            |} [|
                    div [
                        attr.style {|display="flex"; margin="5px"; flexDirection="row";|}
                        
                    ] [
                        addBtn
                        span [attr.style {|fontSize = "2rem"|}] [text $"{this.State.count}"]
                        decrementBtn
                    ]
                |]

    [<SPAEntryPoint>]
    let Main () =
        fetchVal()
        // let root = createRoot (JS.Document.GetElementById "root")
        let root = ReactDom.CreateRoot(JS.Document.GetElementById "root")
        root.Render(React.Make FluentExample ())