namespace WsReactExample.Client.Components

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WsReactExample.Client.Domain

[<JavaScript>]
module Sidebar =
    open WebSharper.React.Html

    type Model = { 
        IsOpen: bool
        CurrentPage: Pages
    }

    type Message =
    | PageChanged of Pages
    | ToggleOpen


    type TabInfo = {
        TabValue:string
        Name:string
        Icon: string

    }
    let init () =
        {
            IsOpen = true
            CurrentPage = Counter
        }, Elmish.Cmd.none

    let update msg model : Model * Elmish.Cmd<Message> =
        match msg with
        | PageChanged page -> {model with CurrentPage=page}, Elmish.Cmd.none
        | ToggleOpen -> {model with IsOpen = not model.IsOpen}, Elmish.Cmd.none

    let pages = [|
        {TabValue="fundraisers";Name="Fundraisers";Icon=Icons.CurrencyDollarEuroRegular}
        {TabValue="counter";Name="Counter";Icon=Icons.CalculatorRegular}
        {TabValue="settings";Name="Settings";Icon=Icons.SettingsRegular}
        {TabValue="showcase";Name="Showcase";Icon=Icons.WindowAppsRegular}
    |]


    [<Inline>]
    let inline private pageTab isOpen ({Icon=icon;TabValue=tabValue;Name=name}:TabInfo) : React.Element =
        JS.jsx $"""<{Components.Tab} key={tabValue} value={tabValue} content={if isOpen then name else ""} icon={{<{icon}/>}}/>"""

    [<Inline>]
    let inline private tabsTitle (isOpen:bool) (title:string) =
        JS.jsx $"""<h4 style={ {|paddingLeft=Styling.tokens.spacingHorizontalM; visibility=if isOpen then "inherit" else "hidden"|} }>{title}</h4>"""
    let [<Inline>] private onToggleClick(dispatch:Elmish.Dispatch<Message>) =
        dispatch ToggleOpen
    let view (model:Model) dispatch : React.Element =
        JS.jsx $"""
            <{Components.TabList} as="nav" vertical size="large" appearance="subtle" style={ {|backgroundColor=Styling.tokens.colorNeutralBackground3 |} } className={if model.IsOpen then "sidebar open" else "sidebar"}
                selectedValue={match model.CurrentPage with
                                | Fundraisers _ -> "fundraisers"
                                | Counter _ -> "counter"
                                | Settings _ -> "settings"
                                | Showcase -> "showcase"}
                // at the time of writing (2023.11), multiple-param lambdas must be wrapped in a System.Action/Func to be properly compiled to JS
                onTabSelect={new System.Action<obj,{|value:string|}>(fun a b -> 
                    let newPage = match b.value with
                                    | "fundraisers" -> Fundraisers
                                    | "settings" -> Settings
                                    | "showcase" -> Showcase
                                    | _ -> Counter
                    if newPage <> model.CurrentPage then dispatch (PageChanged newPage))}>
                <{Components.Tooltip}
                    relationship="label"
                    content="Toggle Sidebar on/off"
                >
                    <{Components.Button} 
                        icon={{<{Icons.LineHorizontal3Regular} fontSize="4rem" />}} appearance="transparent"  
                        style={ {|paddingLeft=Styling.tokens.spacingHorizontalL; paddingRight="0px"; paddingBottom=Styling.tokens.spacingVerticalXS; paddingTop=Styling.tokens.spacingVerticalL; margin="0px"; width="100%" |} }
                        onClick={fun () -> dispatch ToggleOpen}
                    ></{Components.Button}>
                </{Components.Tooltip}>
                {tabsTitle model.IsOpen "Pages"}
                {Array.map (fun p -> pageTab model.IsOpen p) pages}
            </{Components.TabList}>
        """