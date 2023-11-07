namespace WsReactExample.Client.Pages

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React

[<JavaScript>]
module CounterPage =

    type Model = {
        Count: int
    }

    type Message =
    | Increment
    | Decrement
    | Reset

    
    [<Inline>]
    let buttonInline (click: MouseEvent -> unit) (icon:string) (text:string) : React.Element =
        JS.jsx $"""<{Components.CompoundButton} onClick={click} appearance="primary" icon={{<{icon} />}}>
                {text}
            </{Components.CompoundButton}>"""
    
    let update msg model = 
        match msg with
        | Increment -> {model with Count=(model.Count+1)}, Elmish.Cmd.none
        | Decrement -> {model with Count=(model.Count-1)}, Elmish.Cmd.none
        | Reset -> {model with Count =0}, Elmish.Cmd.none
    let init () = 
        {
            Count = 0
        }, Elmish.Cmd.none
    
    let view model dispatch : React.Element =
        Helpers.card [
            "onClick", ignore
            "style", {|rowGap="50px";|}
        ] [
            Helpers.cardHeader [] [Html.h3 [] [Html.text "Counter"]]
            Helpers.cardPreview [] [
                Html.div [
                    "style", {| display="flex"; flexDirection="row"; justifyContent="center"; alignItems="center"; columnGap="20px"|}
                ] [
                    buttonInline (fun _ -> dispatch Increment) Icons.AddRegular "Increment"
                    Html.text $"%i{model.Count}"
                    buttonInline (fun _ -> dispatch Decrement) Icons.SubtractRegular "Decrement"
                ]
            ]
            Helpers.cardFooter [] [
                Html.text "footer helye"
            ]
        ]