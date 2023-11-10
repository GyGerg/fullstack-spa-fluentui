namespace WsReactExample.Client.Components

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WsReactExample.Client.Domain

[<JavaScript>]
module Sidebar =
    open WebSharper.React.Html

    type TabInfo = {
        TabValue:string
        Name:string
        Icon: string
    }
    
    type SidebarItem = 
    | Tab of TabInfo
    | Other of React.Element


    type Model<'PageType when 'PageType: equality> = { 
        IsOpen: bool
        CurrentPage: 'PageType
        Children: SidebarItem array
    }

    type Message<'PageType when 'PageType: equality> =
    | PageChanged of 'PageType
    | ToggleOpen


    
    let pages = [|
        Other(h3 [] [text "Pages"])
        Tab {TabValue="fundraisers";Name="Fundraisers"; Icon=Icons.CurrencyDollarEuroRegular}
        Tab {TabValue="counter";Name="Counter"; Icon=Icons.CalculatorRegular}
        Tab {TabValue="showcase";Name="Showcase"; Icon=Icons.WindowAppsRegular}
        Other(h3 [] [text "Other elements"])
        Tab {TabValue="settings";Name="Settings"; Icon=Icons.SettingsRegular}
    |]

    let init () =
        {
            IsOpen = true
            CurrentPage = Counter
            Children = pages
        }, Elmish.Cmd.none

    let update msg model : Model<'PageType> * Elmish.Cmd<Message<'PageType>> =
        match msg with
        | PageChanged page -> {model with CurrentPage=page}, Elmish.Cmd.none
        | ToggleOpen -> {model with IsOpen = not model.IsOpen}, Elmish.Cmd.none



    [<Inline>]
    let inline private pageTab isOpen ({Icon=icon;TabValue=tabValue;Name=name}:TabInfo) : React.Element =
        JS.jsx $"""<{Components.Tab} 
            style={ {|columnGap=if isOpen then "0.5rem" else "0px"|} }
            key={tabValue} value={tabValue} icon={{<{icon}/>}}><span className="tabText">{name}</span></{Components.Tab}>"""

    [<Inline>]
    let inline private tabsTitle (isOpen:bool) (title:string) =
        JS.jsx $"""<h4 
                        className="hide-on-mobile"
                        style={ {|paddingLeft=Styling.tokens.spacingHorizontalM; visibility=if isOpen then "inherit" else "hidden"|} }>{title}</h4>"""
    let [<Inline>] private onToggleClick(dispatch:Elmish.Dispatch<Message<'PageType>>) =
        dispatch ToggleOpen
    let view<'PageType when 'PageType: equality> (model:Model<'PageType>) dispatch : React.Element =
        let isSmall = WsReactExample.Client.Utils.isMobile()
        let isOpen = if isSmall then false else model.IsOpen
        JS.jsx $"""
            <{Components.TabList} as="nav" vertical size="large" appearance="subtle" style={ {|backgroundColor=Styling.tokens.colorNeutralBackground3 |} } className={if isOpen then "sidebar open" else "sidebar"}
                selectedValue={model.CurrentPage}
                // at the time of writing (2023.11), multiple-param lambdas must be wrapped in a System.Action/Func to be properly compiled to JS
                onTabSelect={new System.Action<obj,{|value:'PageType|}>(fun a b -> 
                    let newPage = b.value
                    if newPage <> model.CurrentPage then dispatch (PageChanged newPage))}>
                <{Components.Tooltip}
                    relationship="label"
                    content="Toggle Sidebar on/off"
                >
                    <{Components.Button} 
                        className="hide-on-mobile"
                        size="large"
                        icon={{<{Icons.LineHorizontal3Regular} as="div" />}} appearance="transparent"  
                        style={ {| margin="0px"; width="100%" |} }
                        onClick={fun () -> dispatch ToggleOpen}
                    ></{Components.Button}>
                </{Components.Tooltip}>
                {tabsTitle isOpen "Pages"}
                {
                    Array.map (function 
                                | Tab p -> pageTab isOpen p
                                | Other elt -> elt) pages
                }
            </{Components.TabList}>
        """