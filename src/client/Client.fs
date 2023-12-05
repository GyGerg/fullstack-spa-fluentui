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
    | SidebarMsg of Components.Sidebar.Message<Domain.Pages>
    | ShowcaseMsg of ShowcasePage.Message
    | ResizedToMobile of bool

    type Model = {
        Counter: CounterPage.Model
        Settings: SettingsPage.Model
        Fundraisers: FundraisersPage.Model
        Sidebar: Components.Sidebar.Model<Domain.Pages>
        Showcase: ShowcasePage.Model

        IsMobile: bool
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

            IsMobile = false
        }, Cmd.none

    // let inline spread a = !...a
    
    let update msg (model:Model) =
        match msg with
        | CounterMsg msg-> 
            let updated,_ = CounterPage.update msg model.Counter
            {model with Counter = updated}, Cmd.none

        | SettingsMsg settingsMsg -> 
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

        | ResizedToMobile isMobile when isMobile <> model.IsMobile -> {model with IsMobile=isMobile} , Cmd.none
        | ResizedToMobile _ -> model, Cmd.none
    
    [<Inline>]
    let tokens = FluentUI.React.Styling.tokens

    let buttonInline (click: MouseEvent -> unit) (icon:string) (text:string) : React.Element =
        JS.jsx $"""<{CompoundButton} onClick={click} appearance="primary" icon={{<{icon} />}}>
                {text}
            </{CompoundButton}>"""

    let view model dispatch = 
        let darkTheme = if model.Settings.UseTeamsTheme then Themes.teamsDarkTheme else Themes.webDarkTheme
        let lightTheme = if model.Settings.UseTeamsTheme then Themes.teamsLightTheme else Themes.webLightTheme
        
        let SettingsRender =
            SettingsPage.view model.Settings (SettingsMsg >> dispatch)

        let mainToolbar = Components.Toolbar.view [] {
            children = [
                Components.Toolbar.ToolbarButton {
                    onClick=None; 
                    Title=Components.Toolbar.Icon (Utils.IconComponent [||] Icons.ArrowLeftRegular)
                }
                Components.Toolbar.ToolbarButton {
                    onClick=None;
                    Title=
                        let icn = 
                            (Utils.IconComponent [|"color", tokens.colorPaletteGreenForeground2|] Icons.MapFilled)
                        Components.Toolbar.TextAndIcon ("Open in Maps", icn)
                }
                Components.Toolbar.WrappedToolbarButton 
                    (Utils.WrapInDialog "" None SettingsRender, {onClick=None;Title=Components.Toolbar.Text "Settings"})
            ]
        }

        let contentToolbar = Components.Toolbar.view ["style", ["backgroundColor", box tokens.colorNeutralBackground3]] {
            children = [
                Components.Toolbar.ToolbarButton {onClick=None; Title=Components.Toolbar.Icon <| Utils.IconComponent [] Icons.ArrowLeftRegular}
                Components.Toolbar.ToolbarButton {onClick=None; Title=Components.Toolbar.TextAndIcon 
                    ("Open in Maps", Utils.IconComponent ["color", tokens.colorPaletteGreenForeground2] Icons.MapFilled)}
                Components.Toolbar.WrappedToolbarButton
                    (Utils.WrapInDialog "sideDialog dialogLeft" None SettingsRender, {onClick=None; Title=Components.Toolbar.Text "Left Popup"})
                Components.Toolbar.WrappedToolbarButton
                    (Utils.WrapInDialog "sideDialog dialogRight" None SettingsRender, {onClick=None; Title=Components.Toolbar.Text "Right Popup"})
            ]
        }
        
        JS.jsx $"""
                    <{FluentProvider} theme={if model.Settings.UseDarkMode then darkTheme else lightTheme}>
                    {Components.Topbar.render model.Settings (SettingsMsg >> dispatch) (nameof(WsReactExample)) }
                    {mainToolbar}
                    <div className="container" style={ {|width="100vw";height="100vh";|} }>
                        {lazyView2 Components.Sidebar.view model.Sidebar (SidebarMsg >> dispatch)}
                        <main className="content">
                            {contentToolbar}
                            <div className="content-container">
                                { 
                                    (
                                        match model.Sidebar.CurrentPage with
                                        | Domain.Counter -> nameof(Domain.Counter), CounterPage.view model.Counter (CounterMsg >> dispatch)
                                        | Domain.Fundraisers -> nameof(Domain.Fundraisers), FundraisersPage.view model.Fundraisers (FundraisersMsg >> dispatch)
                                        | Domain.Settings -> nameof(Domain.Settings), SettingsPage.view model.Settings (SettingsMsg >> dispatch)
                                        | Domain.Showcase -> nameof(Domain.Showcase), ShowcasePage.view model.Showcase (ShowcaseMsg >> dispatch)
                                    )
                                    ||> PageCard.render
                                }
                            </div>
                            <aside className="dialogContainer" id="dialogContainer"></aside>
                        </main>
                    </div>
                    <footer className="footer"><h3>Status: online</h3></footer>
                    </{FluentProvider}>
        """
        

    [<SPAEntryPoint>]
    let Main () =
        
        ServerCommunication.fetchVal()
        
        let sizeSubscribe (model:Model) =
            [ ["resizeEvt"], Utils.resizeSub ResizedToMobile]

        Program.mkProgram init update view
        |> Program.withReactSynchronous "root"
        |> Program.withSubscription sizeSubscribe 
        |> Program.run