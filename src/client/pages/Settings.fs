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
        UseTeamsTheme: bool
    }
    type SettingsMessage = 
    | DarkModeChange of bool
    | ThemeChange of bool


    let view model dispatch =
        
        JS.jsx 
            $"""<div style={ {| display="flex"; flexDirection="column"; flexGrow=true; justifyContent="flex-end";|} }>
            <{Components.Switch} 
                onChange={new System.Action<obj, obj>(fun (evt:obj) (data:obj) -> dispatch (DarkModeChange data?``checked``))}
                checked={model.UseDarkMode}
                label="Use Dark Mode"
            />
            <{Components.Switch} 
                onChange={new System.Action<obj, obj>(fun (evt:obj) (data:obj) -> dispatch (ThemeChange data?``checked``))}
                checked={model.UseTeamsTheme}
                label="Use Teams Theme"
            />
            </div>"""
    let update msg model =
        match msg with
        | DarkModeChange d -> {model with UseDarkMode=d}
        | ThemeChange th -> {model with UseTeamsTheme=th}

        , Elmish.Cmd.none