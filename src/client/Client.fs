namespace WsReactExample.Client

open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.Remoting

[<JavaScript>]
module Client =
    
    let api = Remote<WsReactExample.Shared.IApi>
    let fetchVal() = 
        task {
            let! hello = api.GetValue()
            printfn $"{hello}"
        }
        |> Async.AwaitTask
        |> Async.StartImmediate


    // built-in React.Mount threw some errors around
    type ReactRoot =
        [<Name "render">] 
        member inline _.Render(element:React.Element) : unit = JS.Import("render", "react-dom/client") element

    let inline createRoot (element:Dom.Element) : ReactRoot = JS.Import("createRoot", "react-dom/client") element

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
                    // (JS.Html $"""{{<{FluentUi.Icons.deleteRegular} />}}""") 
                    (React.CreateElement(FluentUi.Icons.deleteRegular, {||}))
                    (fun e -> 
                        this.SetState({this.State with count = (this.State.count - 1)}, fun _ -> printfn $"{this.State.count}")
                        )

            FluentUi.fluentProvider {|
                theme = FluentUi.Themes.teamsLightTheme
            |} [|
                    div [
                        "style", {|display="flex"; margin="5px"; flexDirection="row";|}
                    ] [
                        addBtn
                        span ["style", {|fontSize = "2rem"|}] [text $"{this.State.count}"]
                        decrementBtn
                    ]
                |]

    [<SPAEntryPoint>]
    let Main () =
        Console.Log "test log"
        let root = createRoot (JS.Document.GetElementById "root")
        root.Render(React.Make FluentExample ())

        // React.Make FluentExample ()
        // |> React.Mount (JS.Document.GetElementById "root")