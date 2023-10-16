namespace FluentUi

open WebSharper
open WebSharper.React
open WebSharper.JavaScript
open FluentUi.Bindings
[<JavaScript>]
module Components =
        let accordion props children = React.Element Accordion props children
        let accordionItem props children = React.Element AccordionItem props children
        let accordionHeader props children = React.Element AccordionHeader props children
        let accordionPanel props children = React.Element AccordionPanel props children
        
        
        // avatar-related
        let avatar props = React.Element Avatar props []
        let avatarGroup props children = React.Element AvatarGroup props children

        // badge-related
        let badge props = React.Element Badge props []
        let counterBadge props = React.Element CounterBadge props []
        let presenceBadge props = React.Element PresenceBadge props []

        // button-related
        let button props children = React.Element Button props children
        let compoundButton props children = React.Element CompoundButton props children
        let menuButton props children = React.Element MenuButton props children
        let splitButton props children = React.Element SplitButton props children
        let toggleButton props children = React.Element ToggleButton props children
        
        // card-related
        let card props children = React.Element Card props children
        let cardFooter props children = React.Element CardFooter props children
        let cardHeader props children = React.Element CardHeader props children
        let cardPreview props children = React.Element CardPreview props children
        
        let checkbox props children = React.Element Checkbox props children
        let combobox props children = React.Element Combobox props children
        let option props children = React.Element Option props children
        let listbox props children = React.Element Listbox props children

        let dataGrid props children = React.Element DataGrid props children
        module DataGrid =
            let header props children = React.Element DataGridHeader props children
            let headerCell props children = React.Element DataGridHeaderCell props children
            let body props children = React.Element DataGridBody props children
            let row props children = React.Element DataGridRow props children
            let cell props children = React.Element DataGridCell props children
            let selectionCell props children = React.Element DataGridSelectionCell props children

        let dialog props children = React.Element Dialog props children
        module Dialog =
            let trigger props children = React.Element DialogTrigger props children
            let surface props children = React.Element DialogSurface props children
            let title props children = React.Element DialogTitle props children
            let actions props children = React.Element DialogActions props children

        let divider props children = React.Element Divider props children
        let dropdown props children = React.Element Dropdown props children
        let fluentProvider props children = React.Element FluentProvider props children
        let field props children = React.Element Field props children
        let image props = React.Element Image props []
        let input props = React.Element Input props []
        let label props children = React.Element Label props children
        let link props children = React.Element Link props children
        let menu props children = React.Element Menu props children
        let menuList props children = React.Element MenuList props children
        
        let overflow props children = React.Element Overflow props children
        let overflowItem props children = React.Element OverflowItem props children
        let persona props = React.Element Persona props []
        let popover props children = React.Element Popover props children
        module Popover =
            let popoverTrigger props children = React.Element PopoverTrigger props children
            let popoverSurface props children = React.Element PopoverSurface props children
            let popoverProvider props children = React.Element PopoverProvider props children
        let portal props children = React.Element Portal props children
        let progressBar props = React.Element ProgressBar props []
        
        let radio props = React.Element Radio props []
        let radioGroup props children = React.Element RadioGroup props children
        let select props children = React.Element Select props children
        let skeleton props children = React.Element Skeleton props children
        let skeletonItem props children = React.Element SkeletonItem props children
        let slider props = React.Element Slider props []
        let spinButton props = React.Element SpinButton props []
        let spinner props = React.Element Spinner props []
        let switch props = React.Element Switch props []
        let table props children = React.Element Table props children
        module Table =
            let header props children = React.Element TableHeader props children
            let headerCell props children = React.Element TableHeaderCell props children
            let body props children = React.Element TableBody props children
            let row props children = React.Element TableRow props children
            let cell props children = React.Element TableCell props children
            let selectionCell props children = React.Element TableSelectionCell props children
            let cellLayout props children = React.Element TableCellLayout props children

        let tabList props children = React.Element TabList props children
        let tab props children = React.Element Tab props children

        let tag props children = React.Element Tag props children
        let tagGroup props children = React.Element TagGroup props children
        
        let interactionTag props children = React.Element InteractionTag props children
        module InteractionTag =
            let primary props children = React.Element InteractionTagPrimary props children
            let secondary  props children = React.Element InteractionTagSecondary props children
        let text props children = React.Element Text props children
        let textarea props children = React.Element Textarea props children
        let toast props children = React.Element Toast props children
        module Toast =
            let useToastController toasterId = JS.Import<string -> obj>("useToastController",fluentComponents) toasterId
            let title props children = React.Element ToastTitle props children
            let body props children = React.Element ToastBody props children
            let footer props children = React.Element ToastFooter props children
            let toaster props = React.Element ToastToaster props []

        let toolbar props children = React.Element Toolbar props children
        module Toolbar =
            let button props children = React.Element ToolbarButton props children
            let divider props children = React.Element ToolbarDivider props children
            let group props children = React.Element ToolbarGroup props children
            let radioButton props children = React.Element ToolbarRadioButton props children
            let radioGroup props children = React.Element ToolbarRadioGroup props children
            let toggleButton props children = React.Element ToolbarToggleButton props children

        let tooltip props children = React.Element Tooltip props children
        let tree props children = React.Element Tree props children
        module Tree =
            let item props children = React.Element TreeItem props children
            let itemLayout props children = React.Element TreeItemLayout props children
            let itemPersonaLayout props children = React.Element TreeItemPersonaLayout props children
        let flatTree props children = React.Element FlatTree props children
        module FlatTree =
            let item props children = React.Element FlatTreeItem props children