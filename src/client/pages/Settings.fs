namespace WsReactExample.Client.Pages
open WebSharper
open WebSharper.React
open WebSharper.JavaScript
open WebSharper.FluentUI.React
[<JavaScript>]
module SettingsPage =
    open WebSharper.React.Html
    type Model = {
        UseDarkMode: bool
    }
    type SettingsMessage = 
    | DarkModeChange of bool


    let view model dispatch =
        
        JS.jsx 
            $"""<div style={ {| display="flex"; flexDirection="column"; flexGrow=true; height="100%"; width="100%"|} }>
            <h3>Settings</h3>
            <{Components.Switch} 
                onChange={new System.Action<obj, obj>(fun (evt:obj) (data:obj) -> dispatch (DarkModeChange data?``checked``))}
                checked={model.UseDarkMode}
                label="Use Dark Mode"
            />
            </div>"""
    let update msg model =
        match msg with
        | DarkModeChange d -> {model with UseDarkMode=d}

        , Elmish.Cmd.none