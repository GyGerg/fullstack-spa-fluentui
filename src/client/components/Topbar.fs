namespace WsReactExample.Client.Components

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WsReactExample.Client

[<JavaScript>]
module Topbar =

    type private Notification = {
        Title:string
        Content:string
    }
    let inline private NotificationElt (notification:Notification) = Html.div [] [
        Html.p ["style", {| fontWeight="bold" |} ] [Html.text notification.Title]
        Html.p [] [Html.text notification.Content]
    ]
    
    let [<Inline>] private WrapInPopover children trigger =
        Helpers.popover [
            "size", "small"
            "withArrow", true
        ] [
            Helpers.Popover.popoverTrigger [] [
                trigger
            ]
            Helpers.Popover.popoverSurface [] [
                Html.div [
                    "style", {|
                        display="flex"
                        flexDirection="column"
                        columnGap="0.5rem"
                    |}
                ] children
            ]
        ]


    let [<Inline>] private WrapInMenu children trigger =
        Helpers.menu [] [
            ReactHelpers.Elt MenuTrigger [] [trigger]
            ReactHelpers.Elt MenuPopover [] [
                Helpers.menuList [] children
            ]
        ]

    let [<Inline>] private WrapInSettingsDialog settingsState dispatch trigger =
        Utils.WrapInDialog "dialogClass" (Some <| JS.Document.GetElementsByClassName("container")[0]) (Pages.SettingsPage.view settingsState dispatch) trigger    

    let inline private ProfileButton() =
        JS.jsx $"""
            <{Components.Tooltip} relationship="label" withArrow content="Profile">
                <{Components.Button}
                    icon={{<{Components.Avatar} />}}
                    shape="circular"
                    appearance="transparent"
                />
            </{Components.Tooltip}>
        """
        |> WrapInMenu [
            ReactHelpers.Elt MenuItem [] [Html.text "View profile"]
            ReactHelpers.Elt MenuItem ["color", Styling.tokens.colorPaletteRedForeground1; "style", {|color=Styling.tokens.colorPaletteRedForeground1|}] [Html.text "Log Out"]
        ]

    let inline private SettingsButton textColor settingsState settingsDispatch = 
        JS.jsx $"""
            <{Components.Tooltip} relationship="label" withArrow content="Settings">
                <{Components.Button}
                    icon={{<{Icons.SettingsFilled} color={textColor}/>}}
                    shape="circular"
                    appearance="transparent"
                />
            </{Components.Tooltip}>"""
        |> WrapInSettingsDialog settingsState settingsDispatch
    let inline private NotificationsButton textColor notifications =
        JS.jsx $"""
                <{Components.Tooltip} relationship="label" withArrow content="Notifications" >
                    <{Components.Button}
                        icon={{<{Icons.AlertBadgeFilled} color={textColor}/>}}
                        shape="circular"
                        appearance="transparent"
                    />
                </{Components.Tooltip}>
        """
        |> WrapInPopover (notifications |> Seq.map NotificationElt)

    let render (settingsState:WsReactExample.Client.Pages.SettingsPage.Model) settingsDispatch (title:string) : React.Element =

        let backgroundColorToken = if settingsState.UseDarkMode then Styling.tokens.colorBrandBackground2 else Styling.tokens.colorBrandBackground
        let textColorToken = if settingsState.UseDarkMode then Styling.tokens.colorBrandForeground2 else Styling.tokens.colorNeutralForegroundInverted
        JS.jsx $"""<header className="header" style={ {|
            backgroundColor=backgroundColorToken; 
            color=textColorToken |} } >
            <section style={ {|display="flex"; flexDirection="row"; columnGap="0.5rem"|} }>
                <{Components.Button} appearance="transparent" icon={{<{Icons.GridDotsFilled} color={textColorToken} as="div" />}} size="large" />
                <h3>{title}</h3>
            </section>
            <menu style={ {|display="flex"; flexDirection="row"; justifyContent="end"; alignItems="center"; columnGap="0.5rem"; paddingRight="0.5rem" |} }>
                {SettingsButton textColorToken settingsState settingsDispatch}
                {NotificationsButton textColorToken [
                    {Title="Notification";Content="Congrats for your notification!"}
                    {Title="Another one";Content="Wow someone's famous!"}
                ]}
                {ProfileButton()}
            </menu>
        </header>"""