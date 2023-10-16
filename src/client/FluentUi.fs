namespace FluentUi
    open WebSharper.JavaScript
    open WebSharper.React
    open WebSharper
    [<AutoOpen; JavaScript; Sealed>]
    module internal Constants =
        let [<Literal>] fluentUi = "@fluentui"
        let [<Literal>] fluentComponents = fluentUi+"/react-components"
        let [<Literal>] fluentIcons = fluentUi+"/react-icons"
    
    [<JavaScript>]
    module Themes =
        let teamsLightTheme = JS.Import<obj>("teamsLightTheme", fluentComponents)
        let teamsDarkTheme = JS.Import<obj>("teamsDarkTheme", fluentComponents)
    
    [<JavaScript>]
    module Icons =
        [<Inline;>] 
        let AddRegular<'a> = JS.Import<'a>("AddRegular", fluentIcons)
        [<Inline;>] 
        let DeleteRegular<'a> = JS.Import<'a>("DeleteRegular", fluentIcons)
        
    [<JavaScript>]
    module Bindings =
        let [<Inline>] Accordion<'a> = JS.Import<'a>("Accordion",fluentComponents)
        
        // module [<AutoOpen>] Accordion =
        let [<Inline>] AccordionItem<'a> = JS.Import<'a>("AccordionItem", fluentComponents)
        let [<Inline>] AccordionHeader<'a> = JS.Import<'a>("AccordionHeader",fluentComponents)
        let [<Inline>] AccordionPanel<'a> = JS.Import<'a>("AccordionPanel", fluentComponents)
        
        let [<Inline>] Avatar<'a> = JS.Import<'a>( "Avatar",fluentComponents)
        let [<Inline>] AvatarGroup<'a> = JS.Import<'a>( "AvatarGroup",fluentComponents)

        let [<Inline>] Badge<'a> = JS.Import<'a>("Badge",fluentComponents)
        let [<Inline>] CounterBadge<'a> = JS.Import<'a>( "CounterBadge",fluentComponents)
        let [<Inline>] PresenceBadge<'a> = JS.Import<'a>( "PresenceBadge",fluentComponents)

        
        let [<Inline>] Button<'a> = JS.Import<'a>("Button",fluentComponents)
        let [<Inline>] CompoundButton<'a> = JS.Import<'a>("CompoundButton",fluentComponents)
        let [<Inline>] MenuButton<'a> = JS.Import<'a>("MenuButton",fluentComponents)
        let [<Inline>] SplitButton<'a> = JS.Import<'a>("SplitButton",fluentComponents)
        let [<Inline>] ToggleButton<'a> = JS.Import<'a>("ToggleButton",fluentComponents)

        let [<Inline>] Card<'a> = JS.Import<'a>("Card",fluentComponents)
        let [<Inline>] CardHeader<'a> = JS.Import<'a>("CardFooter",fluentComponents)
        let [<Inline>] CardFooter<'a> = JS.Import<'a>("CardHeader",fluentComponents)
        let [<Inline>] CardPreview<'a> = JS.Import<'a>("CardPreview",fluentComponents)

        let [<Inline>] Checkbox<'a> = JS.Import<'a>("Checkbox", fluentComponents)
        let [<Inline>] Combobox<'a> = JS.Import<'a>("Combobox", fluentComponents)
        
        let [<Inline>] Option<'a> = JS.Import<'a>("Option", fluentComponents)
        let [<Inline>] Listbox<'a> = JS.Import<'a>("Listbox", fluentComponents)

        
        let [<Inline>] DataGrid<'a> = JS.Import<'a>("DataGrid", fluentComponents)
        let [<Inline>] DataGridHeader<'a> = JS.Import<'a>("DataGridHeader", fluentComponents)
        let [<Inline>] DataGridHeaderCell<'a> = JS.Import<'a>("DataGridHeaderCell", fluentComponents)
        let [<Inline>] DataGridBody<'a> = JS.Import<'a>("DataGridBody", fluentComponents)
        let [<Inline>] DataGridRow<'a> = JS.Import<'a>("DataGridRow", fluentComponents)
        let [<Inline>] DataGridCell<'a> = JS.Import<'a>("DataGridCell", fluentComponents)
        let [<Inline>] DataGridSelectionCell<'a> = JS.Import<'a>("DataGridSelectionCell", fluentComponents)

        let [<Inline>] Dialog<'a> = JS.Import<'a>("Dialog", fluentComponents)
        // module [<AutoOpen>] Dialog =
        let [<Inline>] DialogTrigger<'a> = JS.Import<'a>("DialogTrigger", fluentComponents)
        let [<Inline>] DialogSurface<'a> = JS.Import<'a>("DialogSurface", fluentComponents)
        let [<Inline>] DialogTitle<'a> = JS.Import<'a>("DialogTitle", fluentComponents)
        let [<Inline>] DialogActions<'a> = JS.Import<'a>("DialogActions", fluentComponents)

        let [<Inline>] Divider<'a> = JS.Import<'a>("Divider", fluentComponents)
        let [<Inline>] Dropdown<'a> = JS.Import<'a>("Dropdown", fluentComponents)
        
        let [<Inline>] FluentProvider<'a> = JS.Import<'a>("FluentProvider", fluentComponents)
        let [<Inline>] Field<'a> = JS.Import<'a>("Field", fluentComponents)
        let [<Inline>] Image<'a> = JS.Import<'a>("Image", fluentComponents)
        let [<Inline>] Input<'a> = JS.Import<'a>("Input", fluentComponents)
        let [<Inline>] Label<'a> = JS.Import<'a>("Label", fluentComponents)
        let [<Inline>] Link<'a> = JS.Import<'a>("Link", fluentComponents)
        let [<Inline>] Menu<'a> = JS.Import<'a>("Menu", fluentComponents)
        let [<Inline>] MenuList<'a> = JS.Import<'a>("MenuList", fluentComponents)
        
        let [<Inline>] Overflow<'a> = JS.Import<'a>("Overflow", fluentComponents)
        let [<Inline>] OverflowItem<'a> = JS.Import<'a>("OverflowItem", fluentComponents)
        let [<Inline>] Persona<'a> = JS.Import<'a>("Persona", fluentComponents)

        let [<Inline>] Popover<'a> = JS.Import<'a>("Popover",fluentComponents)
        // module [<AutoOpen>] Popover =
        let [<Inline>] PopoverTrigger<'a> = JS.Import<'a>("PopoverTrigger",fluentComponents)
        let [<Inline>] PopoverSurface<'a> = JS.Import<'a>("PopoverSurface",fluentComponents)
        let [<Inline>] PopoverProvider<'a> = JS.Import<'a>("PopoverProvider",fluentComponents)
        let [<Inline>] Portal<'a> = JS.Import<'a>("Portal",fluentComponents)
        let [<Inline>] ProgressBar<'a> = JS.Import<'a>("ProgressBar", fluentComponents)

        let [<Inline>] Radio<'a> = JS.Import<'a>("Radio", fluentComponents)
        let [<Inline>] RadioGroup<'a> = JS.Import<'a>("RadioGroup", fluentComponents)
        let [<Inline>] Select<'a> = JS.Import<'a>("Select", fluentComponents)
        let [<Inline>] Skeleton<'a> = JS.Import<'a>("Skeleton", fluentComponents)
        // module [<AutoOpen>] Skeleton =
        let [<Inline>] SkeletonItem<'a> = JS.Import<'a>("SkeletonItem", fluentComponents)
        let [<Inline>] Slider<'a> = JS.Import<'a>("Slider", fluentComponents)
        let [<Inline>] SpinButton<'a> = JS.Import<'a>("SpinButton", fluentComponents)
        let [<Inline>] Spinner<'a> = JS.Import<'a>("Spinner", fluentComponents)
        let [<Inline>] Switch<'a> = JS.Import<'a>("Switch", fluentComponents)
        let [<Inline>] Table<'a> = JS.Import<'a>("Table", fluentComponents)
        // module [<AutoOpen>] Table =
        let [<Inline>] TableHeader<'a> = JS.Import<'a>("TableHeader", fluentComponents)
        let [<Inline>] TableHeaderCell<'a> = JS.Import<'a>("TableHeaderCell", fluentComponents)
        let [<Inline>] TableBody<'a> = JS.Import<'a>("TableBody", fluentComponents)
        let [<Inline>] TableRow<'a> = JS.Import<'a>("TableRow", fluentComponents)
        let [<Inline>] TableCell<'a> = JS.Import<'a>("TableCell", fluentComponents)
        let [<Inline>] TableSelectionCell<'a> = JS.Import<'a>("TableSelectionCell", fluentComponents)
        let [<Inline>] TableCellLayout<'a> = JS.Import<'a>("TableCellLayout", fluentComponents)

        let [<Inline>] TabList<'a> = JS.Import<'a>("TabList", fluentComponents)
        let [<Inline>] Tab<'a> = JS.Import<'a>("Tab", fluentComponents)

        let [<Inline>] Tag<'a> = JS.Import<'a>("Tag", fluentComponents)
        let [<Inline>] TagGroup<'a> = JS.Import<'a>("TagGroup", fluentComponents)
        
        let [<Inline>] InteractionTag<'a> = JS.Import<'a>("InteractionTag", fluentComponents)
        // module [<AutoOpen>] InteractionTag =
        let [<Inline>] InteractionTagPrimary<'a> = JS.Import<'a>("InteractionTagPrimary", fluentComponents)
        let [<Inline>] InteractionTagSecondary<'a> = JS.Import<'a>("InteractionTagSecondary", fluentComponents)
        let [<Inline>] Text<'a> = JS.Import<'a>("Text", fluentComponents)
        let [<Inline>] Textarea<'a> = JS.Import<'a>("Textarea", fluentComponents)
        let [<Inline>] Toast<'a> = JS.Import<'a>("Toast", fluentComponents)
        // module [<AutoOpen>] Toast =
        let [<Inline>] useToastController toasterId = JS.Import<string -> obj>("useToastController",fluentComponents)
        let [<Inline>] ToastTitle<'a> = JS.Import<'a>("ToastTitle", fluentComponents)
        let [<Inline>] ToastBody<'a> = JS.Import<'a>("ToastBody", fluentComponents)
        let [<Inline>] ToastFooter<'a> = JS.Import<'a>("ToastFooter", fluentComponents)
        let [<Inline>] ToastToaster<'a> = JS.Import<'a>("Toaster", fluentComponents)

        let [<Inline>] Toolbar<'a> = JS.Import<'a>("Toolbar", fluentComponents)
        // module [<AutoOpen>] Toolbar =
        let [<Inline>] ToolbarButton<'a> = JS.Import<'a>("ToolbarButton", fluentComponents)
        let [<Inline>] ToolbarDivider<'a> = JS.Import<'a>("ToolbarDivider", fluentComponents)
        let [<Inline>] ToolbarGroup<'a> = JS.Import<'a>("ToolbarGroup", fluentComponents)
        let [<Inline>] ToolbarRadioButton<'a> = JS.Import<'a>("ToolbarRadioButton", fluentComponents)
        let [<Inline>] ToolbarRadioGroup<'a> = JS.Import<'a>("ToolbarRadioGroup", fluentComponents)
        let [<Inline>] ToolbarToggleButton<'a> = JS.Import<'a>("ToolbarToggleButton", fluentComponents)

        let [<Inline>] Tooltip<'a> = JS.Import<'a>("Tooltip", fluentComponents)
        let [<Inline>] Tree<'a> = JS.Import<'a>("Tree", fluentComponents)
        // module [<AutoOpen>] Tree =
        let [<Inline>] TreeItem<'a> = JS.Import<'a>("TreeItem", fluentComponents)
        let [<Inline>] TreeItemLayout<'a> = JS.Import<'a>("TreeItemLayout", fluentComponents)
        let [<Inline>] TreeItemPersonaLayout<'a> = JS.Import<'a>("TreeItemPersonaLayout", fluentComponents)
        let [<Inline>] FlatTree<'a> = JS.Import<'a>("FlatTree", fluentComponents)
        // module [<AutoOpen>] FlatTree =
        let [<Inline>] FlatTreeItem<'a> = JS.Import<'a>("FlatTreeItem", fluentComponents)


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
            React.Element (JS.Import( "FluentProvider",fluentComponents)) props children
