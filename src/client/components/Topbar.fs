namespace WsReactExample.Client.Components

open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React

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

    let rec [<Inline>] private MenuTrigger<'a> = JS.Import<'a>(nameof(MenuTrigger), "@fluentui/react-components")
    let rec [<Inline>] private MenuPopover<'a> = JS.Import<'a>(nameof(MenuPopover), "@fluentui/react-components")
    let rec [<Inline>] private MenuItem<'a> = JS.Import<'a>(nameof(MenuItem), "@fluentui/react-components")

    let rec [<Inline>] private DialogBody<'a> = JS.Import<'a>(nameof(DialogBody), "@fluentui/react-components")
    let rec [<Inline>] private DialogContent<'a> = JS.Import<'a>(nameof(DialogContent), "@fluentui/react-components")

    let [<Inline>] private WrapInMenu children trigger =
        Helpers.menu [] [
            ReactHelpers.Elt MenuTrigger [] [trigger]
            ReactHelpers.Elt MenuPopover [] [
                Helpers.menuList [] children
            ]
        ]

    let [<Inline>] private WrapInSettingsDialog settingsState dispatch trigger =
        Helpers.dialog [
            "modalType", "non-modal"
        ] [
            Helpers.Dialog.trigger [] [trigger]
            Helpers.Dialog.surface [] [
                ReactHelpers.Elt DialogContent [] [
                    Helpers.Dialog.title [] [
                        // Html.h3 [] [Html.text "Settings"]
                    ]
                    ReactHelpers.Elt DialogContent [] [
                        WsReactExample.Client.Pages.SettingsPage.view settingsState dispatch  
                    ]
                    Helpers.Dialog.actions [
                        "fluid", true
                    ] [
                        // Helpers.button ["appearance", "secondary"] [Html.text "Close"]
                    ]
                ]
            ]
        ]

    

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

    let inline private SettingsButton settingsState settingsDispatch = 
        JS.jsx $"""
            <{Components.Tooltip} relationship="label" withArrow content="Settings">
                <{Components.Button}
                    icon={{<{Icons.SettingsFilled}/>}}
                    shape="circular"
                    appearance="transparent"
                />
            </{Components.Tooltip}>"""
        |> WrapInSettingsDialog settingsState settingsDispatch

    let inline private NotificationsButton notifications =
        JS.jsx $"""
                <{Components.Tooltip} relationship="label" withArrow content="Notifications">
                    <{Components.Button}
                        icon={{<{Icons.AlertBadgeFilled}/>}}
                        shape="circular"
                        appearance="transparent"
                    />
                </{Components.Tooltip}>
        """
        |> WrapInPopover (notifications |> Seq.map NotificationElt)
    let render settingsState settingsDispatch (title:string) : React.Element =
        JS.jsx $"""<header className="header" style={ {|backgroundColor=Styling.tokens.colorBrandBackground |} } >
            <h3>{title}</h3>
            <div style={ {|marginLeft="auto"; display="flex"; flexDirection="row"; justifyContent="end"; alignItems="center"; columnGap="0.5rem"; paddingRight="1rem" |} }>
                {SettingsButton settingsState settingsDispatch}
                {NotificationsButton [
                    {Title="Notification";Content="Congrats for your notification!"}
                    {Title="Another one";Content="Wow someone's famous!"}
                ]}
                {ProfileButton()}
            </div>
        </header>"""