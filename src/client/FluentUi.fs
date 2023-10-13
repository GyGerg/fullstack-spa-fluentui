namespace WsReactExample.Client

open WebSharper.JavaScript
open WebSharper.React
open WebSharper
[<JavaScript;AutoOpen; Sealed>]
module private Constants =
    let [<Literal>] fluentUi = "@fluentui"
    let [<Literal>] fluentComponents = fluentUi+"/react-components"
    let [<Literal>] fluentIcons = fluentUi+"/react-icons"

[<JavaScript;Import(fluentComponents);Sealed>]
module FluentUi =
    module Themes =
        let teamsLightTheme = JS.Import<obj>("teamsLightTheme", fluentComponents)
        let teamsDarkTheme = JS.Import<obj>("teamsDarkTheme", fluentComponents)

    module Icons =
        [<Inline; Name "AddRegular">] 
        let addRegular = JS.Import<obj>("AddRegular", fluentIcons)
        [<Inline; Name "DeleteRegular">] 
        let deleteRegular = JS.Import<obj>("DeleteRegular", fluentIcons)
    // [<Name "CompoundButton">] 
    let inline propsEl name props = React.Element name props []
    
    module Components =
    // accordion-related
        let accordion props children = React.Element (JS.Import("Accordion",fluentComponents)) props children
        let accordionItem props children = React.Element (JS.Import("AccordionItem",fluentComponents)) props children
        let accordionHeader props children = React.Element (JS.Import("AccordionHeader",fluentComponents)) props children
        let accordionPanel props children = React.Element (JS.Import("AccordionPanel",fluentComponents)) props children
        
        // avatar-related
        let avatar props = React.Element (JS.Import( "Avatar",fluentComponents)) props []
        let avatarGroup props children = React.Element (JS.Import( "AvatarGroup",fluentComponents)) props children

        // badge-related
        let badge props = propsEl (JS.Import("Badge",fluentComponents)) props
        let counterBadge props = propsEl (JS.Import( "CounterBadge",fluentComponents)) props
        let presenceBadge props = propsEl (JS.Import( "PresenceBadge",fluentComponents)) props

        // button-related
        let button props children = React.Element (JS.Import("Button",fluentComponents)) props children
        let compoundButton props children = React.Element (JS.Import("CompoundButton",fluentComponents)) props children
        let menuButton props children = React.Element (JS.Import("MenuButton",fluentComponents)) props children
        let splitButton props children = React.Element (JS.Import("SplitButton",fluentComponents)) props children
        let toggleButton props children = React.Element (JS.Import("ToggleButton",fluentComponents)) props children
        
        // card-related
        let card props children = React.Element (JS.Import("Card",fluentComponents)) props children
        let cardFooter props children = React.Element (JS.Import("CardFooter",fluentComponents)) props children
        let cardHeader props children = React.Element (JS.Import("CardHeader",fluentComponents)) props children
        let cardPreview props children = React.Element (JS.Import("CardPreview",fluentComponents)) props children
        
        let checkbox props children = React.Element (JS.Import("Checkbox", fluentComponents)) props children
        let combobox props children = React.Element (JS.Import("Combobox", fluentComponents)) props children
        module Combobox =
            let option props children = React.Element (JS.Import("Option", fluentComponents)) props children
            let listbox props children = React.Element (JS.Import("Listbox", fluentComponents)) props children

        let dataGrid props children = React.Element (JS.Import("DataGrid", fluentComponents)) props children
        module DataGrid =
            let header props children = React.Element (JS.Import("DataGridHeader", fluentComponents)) props children
            let headerCell props children = React.Element (JS.Import("DataGridHeaderCell", fluentComponents)) props children
            let body props children = React.Element (JS.Import("DataGridBody", fluentComponents)) props children
            let row props children = React.Element (JS.Import("DataGridRow", fluentComponents)) props children
            let cell props children = React.Element (JS.Import("DataGridCell", fluentComponents)) props children
            let selectionCell props children = React.Element (JS.Import("DataGridSelectionCell", fluentComponents)) props children
        let dialog props children = React.Element (JS.Import("Dialog", fluentComponents)) props children
        module Dialog =
            let trigger props children = React.Element (JS.Import("DialogTrigger", fluentComponents)) props children
            let surface props children = React.Element (JS.Import("DialogSurface", fluentComponents)) props children
            let title props children = React.Element (JS.Import("DialogTitle", fluentComponents)) props children
            let actions props children = React.Element (JS.Import("DialogActions", fluentComponents)) props children

        let divider props children = React.Element (JS.Import("Divider", fluentComponents)) props children
        let dropdown props children = React.Element (JS.Import("Dropdown", fluentComponents)) props children
        module Dropdown =
            let option props children = React.Element (JS.Import("DropdownOption", fluentComponents)) props children
            let listbox props children = React.Element (JS.Import("ListBox", fluentComponents)) props children
        let field props children = React.Element (JS.Import("Field", fluentComponents)) props children
        let image props = React.Element (JS.Import("Image", fluentComponents)) props []
        let input props = React.Element (JS.Import("Input", fluentComponents)) props []
        let label props children = React.Element (JS.Import("Label", fluentComponents)) props children
        let link props children = React.Element (JS.Import("Link", fluentComponents)) props children
        let menu props children = React.Element (JS.Import("Menu", fluentComponents)) props children
        let menuList props children = React.Element (JS.Import("MenuList", fluentComponents)) props children
        
        let overflow props children = React.Element (JS.Import("Overflow", fluentComponents)) props children
        let overflowItem props children = React.Element (JS.Import("OverflowItem", fluentComponents)) props children
        let persona props = React.Element (JS.Import("Persona", fluentComponents)) props []
        let popover props children = React.Element (JS.Import("Popover",fluentComponents)) props children
        module Popover =
            let popoverTrigger props children = React.Element (JS.Import("PopoverTrigger",fluentComponents)) props children
            let popoverSurface props children = React.Element (JS.Import("PopoverSurface",fluentComponents)) props children
            let popoverProvider props children = React.Element (JS.Import("PopoverProvider",fluentComponents)) props children
        let portal props children = React.Element (JS.Import("Portal",fluentComponents)) props children
        let progressBar props = React.Element (JS.Import("ProgressBar", fluentComponents)) props []
        
        let radio props = React.Element (JS.Import("Radio", fluentComponents)) props []
        let radioGroup props children = React.Element (JS.Import("RadioGroup", fluentComponents)) props children
        let select props children = React.Element (JS.Import("Select", fluentComponents)) props children
        let skeleton props children = React.Element (JS.Import("Skeleton", fluentComponents)) props children
        let skeletonItem props children = React.Element (JS.Import("SkeletonItem", fluentComponents)) props children
        let slider props = React.Element (JS.Import("Slider", fluentComponents)) props []
        let spinButton props = React.Element (JS.Import("SpinButton", fluentComponents)) props []
        let spinner props = React.Element (JS.Import("Spinner", fluentComponents)) props []
        let switch props = React.Element (JS.Import("Switch", fluentComponents)) props []
        let table props children = React.Element (JS.Import("Table", fluentComponents)) props children
        module Table =
            let header props children = React.Element (JS.Import("TableHeader", fluentComponents)) props children
            let headerCell props children = React.Element (JS.Import("TableHeaderCell", fluentComponents)) props children
            let body props children = React.Element (JS.Import("TableBody", fluentComponents)) props children
            let row props children = React.Element (JS.Import("TableRow", fluentComponents)) props children
            let cell props children = React.Element (JS.Import("TableCell", fluentComponents)) props children
            let selectionCell props children = React.Element (JS.Import("TableSelectionCell", fluentComponents)) props children
            let cellLayout props children = React.Element (JS.Import("TableCellLayout", fluentComponents)) props children

        let tabList props children = React.Element (JS.Import("TabList", fluentComponents)) props children
        let tab props children = React.Element (JS.Import("Tab", fluentComponents)) props children

        let tag props children = React.Element (JS.Import("Tag", fluentComponents)) props children
        let tagGroup props children = React.Element (JS.Import("TagGroup", fluentComponents)) props children
        
        let interactionTag props children = React.Element (JS.Import("InteractionTag", fluentComponents)) props children
        module InteractionTag =
            let primary props children = React.Element (JS.Import("InteractionTagPrimary", fluentComponents)) props children
            let secondary  props children = React.Element (JS.Import("InteractionTagSecondary", fluentComponents)) props children
        let text props children = React.Element (JS.Import("Text", fluentComponents)) props children
        let textarea props children = React.Element (JS.Import("Textarea", fluentComponents)) props children
        let toast props children = React.Element (JS.Import("Toast", fluentComponents)) props children
        module Toast =
            let useToastController toasterId = JS.Import<string -> obj>("useToastController",fluentComponents)
            let title props children = React.Element (JS.Import("ToastTitle", fluentComponents)) props children
            let body props children = React.Element (JS.Import("ToastBody", fluentComponents)) props children
            let footer props children = React.Element (JS.Import("ToastFooter", fluentComponents)) props children
            let toaster props = React.Element (JS.Import("Toaster", fluentComponents)) props []

        let toolbar props children = React.Element (JS.Import("Toolbar", fluentComponents)) props children
        module Toolbar =
            let button props children = React.Element (JS.Import("ToolbarButton", fluentComponents)) props children
            let divider props children = React.Element (JS.Import("ToolbarDivider", fluentComponents)) props children
            let group props children = React.Element (JS.Import("ToolbarGroup", fluentComponents)) props children
            let radioButton props children = React.Element (JS.Import("ToolbarRadioButton", fluentComponents)) props children
            let radioGroup props children = React.Element (JS.Import("ToolbarRadioGroup", fluentComponents)) props children
            let toggleButton props children = React.Element (JS.Import("ToolbarToggleButton", fluentComponents)) props children

        let tooltip props children = React.Element (JS.Import("Tooltip", fluentComponents)) props children
        let tree props children = React.Element (JS.Import("Tree", fluentComponents)) props children
        module Tree =
            let item props children = React.Element (JS.Import("TreeItem", fluentComponents)) props children
            let itemLayout props children = React.Element (JS.Import("TreeItemLayout", fluentComponents)) props children
            let itemPersonaLayout props children = React.Element (JS.Import("TreeItemPersonaLayout", fluentComponents)) props children
            let flat props children = React.Element (JS.Import("FlatTree", fluentComponents)) props children
            module Flat =
                let item props children = React.Element (JS.Import("FlatTreeItem", fluentComponents)) props children

    [<Inline>] 
    let CompoundButton = JS.Import<React.Component<obj,obj>>("CompoundButton",fluentComponents)
    
    let compoundButtonFunc (props: seq<string*obj>) ([<System.ParamArray>] children: React.Element seq) = React.CreateElement(CompoundButton, props, Array.ofSeq children)
    
    let compoundButtonInline (text:string) (secondaryText:string) (icon:obj) (onClick: Bindings.MouseEvent -> unit) : React.Element = JS.Html $"""
        <{CompoundButton} secondaryContent={secondaryText} onClick={onClick} appearance="primary" icon={icon} >
            {text}
        </{CompoundButton}>
    """
    // [<Name "FluentProvider">] 
    // let inline fluentProvider (props: seq<string*obj>) ([<System.ParamArray>] children: React.Element seq) = 
    //     React.CreateElement(JS.Import<obj>("FluentProvider",fluentComponents), Array.ofSeq props, Array.ofSeq children)
    [<Inline; Name "FluentProvider">]
    let fluentProvider (props:seq<string*obj>) (children: seq<React.Element>) = 
        // React.CreateElement(JS.Import<ElCreate>("FluentProvider",fluentComponents), props, Array.ofSeq children)
        React.Element (JS.Import( "FluentProvider",fluentComponents)) props children
