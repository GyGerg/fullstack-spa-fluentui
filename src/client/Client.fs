namespace WsReactExample.Client

open Elmish
open WebSharper
open WebSharper.React.Html
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.Elmish.React
open WebSharper.FluentUI.React.Components
module Icons = FluentUI.React.Icons
open WebSharper.FluentUI.React.Styling

open WsReactExample.Client.Pages

[<JavaScript>]
module Client =
    type Message = 
    | CounterMsg of CounterPage.Message
    | SettingsMsg of SettingsPage.SettingsMessage
    | FundraisersMsg of FundraisersPage.Message
    | SidebarMsg of Components.Sidebar.Message
    | ShowcaseMsg of ShowcasePage.Message

    type Model = {
        Counter: CounterPage.Model
        Settings: SettingsPage.Model
        Fundraisers: FundraisersPage.Model
        Sidebar: Components.Sidebar.Model
        Showcase: ShowcasePage.Model
    }

    let init () = 
        { 
            Counter = fst <| CounterPage.init()
            Settings = {
                UseDarkMode = false
                UseTeamsTheme = false
            }
            Fundraisers = {
                Fundraisers = [||]
            }
            Sidebar = fst <| Components.Sidebar.init()
            Showcase = fst <| ShowcasePage.init()
        }, Cmd.none

    // let inline spread a = !...a
    
    let update msg (model:Model) =
        match msg with
        | CounterMsg msg-> 
            let updated,_ = CounterPage.update msg model.Counter
            {model with Counter = updated}, Cmd.none

        | SettingsMsg ( settingsMsg) -> 
            let newSettings, _ = SettingsPage.update settingsMsg model.Settings
            {model with Settings = newSettings}, Cmd.none
        | FundraisersMsg fundraisersMsg ->
            let newFundraisers, fundraisersCmd = FundraisersPage.update fundraisersMsg model.Fundraisers
            {model with Fundraisers = newFundraisers}, Cmd.map FundraisersMsg fundraisersCmd
        | ShowcaseMsg showcaseMsg ->
            let newShowcase, showcaseCmd = ShowcasePage.update showcaseMsg model.Showcase
            {model with Showcase = newShowcase}, showcaseCmd
        | SidebarMsg sidebarMsg -> 
            let newSidebar, sidebarCmd = Components.Sidebar.update (sidebarMsg) model.Sidebar
            match sidebarMsg with
            | Components.Sidebar.PageChanged pageMsg ->
                match pageMsg with
                | Domain.Pages.Fundraisers when model.Fundraisers.Fundraisers.Length = 0 ->
                    let newFundraisers, fundraisersMsg = FundraisersPage.init()
                    {model with Sidebar=newSidebar; Fundraisers = newFundraisers}, Cmd.batch [|(Cmd.map SidebarMsg sidebarCmd); (Cmd.map FundraisersMsg fundraisersMsg)|]
                | Domain.Pages.Counter ->
                    let counterState, _ = CounterPage.init()
                    {model with Sidebar=newSidebar; Counter = counterState}, (Cmd.map SidebarMsg sidebarCmd)
                | _ ->
                    {model with Sidebar=newSidebar}, (Cmd.map SidebarMsg sidebarCmd)
            | Components.Sidebar.ToggleOpen -> 
                printfn $"Is sidebar open: {newSidebar.IsOpen}"
                {model with Sidebar=newSidebar}, Cmd.none
    
    [<Inline>]
    let tokens = FluentUI.React.Styling.tokens

    [<Inline>]
    let buttonInline (click: MouseEvent -> unit) (icon:string) (text:string) : React.Element =
        JS.jsx $"""<{CompoundButton} onClick={click} appearance="primary" icon={{<{icon} />}}>
                {text}
            </{CompoundButton}>"""

    
    let view model dispatch = 
        let darkTheme = if model.Settings.UseTeamsTheme then Themes.teamsDarkTheme else Themes.webDarkTheme
        let lightTheme = if model.Settings.UseTeamsTheme then Themes.teamsLightTheme else Themes.webLightTheme
        JS.jsx $"""
            
            <{FluentProvider} theme={if model.Settings.UseDarkMode then darkTheme else lightTheme}>
                <div className="fluentRoot">
                    {Components.Topbar.render model.Settings (SettingsMsg >> dispatch) (nameof(WsReactExample)) }
                    {lazyView2 Components.Sidebar.view model.Sidebar (SidebarMsg >> dispatch)}
                    <{Toolbar} className="menu" style={ {|backgroundColor=tokens.colorNeutralBackground2|} }>
                        <{ToolbarButton} icon={{<{Icons.ArrowLeftRegular} />}} >Back</{ToolbarButton}>
                        <{ToolbarButton} icon={{<{Icons.MapFilled} color={tokens.colorPaletteGreenForeground2}/>}}>Open in Maps</{ToolbarButton}>
                        <{ToolbarButton} >Button 1</{ToolbarButton}>
                        <{ToolbarButton} >Button 2</{ToolbarButton}>
                        <{ToolbarButton} >Button 3</{ToolbarButton}>
                        <{ToolbarButton} >Button 4</{ToolbarButton}>
                    </{Toolbar}>
                    <div className="content" >
                        { 
                            
                            (
                                match model.Sidebar.CurrentPage with
                                | Domain.Pages.Counter -> lazyView2 CounterPage.view model.Counter (CounterMsg >> dispatch)
                                | Domain.Pages.Fundraisers -> lazyView2 FundraisersPage.view model.Fundraisers (FundraisersMsg >> dispatch)
                                | Domain.Pages.Settings -> lazyView2 SettingsPage.view model.Settings (SettingsMsg >> dispatch)
                                | Domain.Pages.Showcase -> ShowcasePage.view model.Showcase (ShowcaseMsg >> dispatch)
                            )
                        }
                        <aside className="dialogContainer" id="dialogContainer"></aside>
                    </div>
                </div>
            </{FluentProvider}>
        """
        

    [<SPAEntryPoint>]
    let Main () =
        
        ServerCommunication.fetchVal()
        
        Program.mkProgram init update view
        |> Program.withReactSynchronous "root"
        |> Program.run