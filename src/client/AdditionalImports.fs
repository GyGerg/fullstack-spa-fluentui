namespace WsReactExample.Client
open WebSharper
open WebSharper.JavaScript

[<AutoOpen>]
module internal AdditionalImports =
    
    let rec [<Inline>] MenuTrigger<'a> = JS.Import<'a>(nameof(MenuTrigger), "@fluentui/react-components")
    let rec [<Inline>] MenuPopover<'a> = JS.Import<'a>(nameof(MenuPopover), "@fluentui/react-components")
    let rec [<Inline>] MenuItem<'a> = JS.Import<'a>(nameof(MenuItem), "@fluentui/react-components")

    let rec [<Inline>] DialogBody<'a> = JS.Import<'a>(nameof(DialogBody), "@fluentui/react-components")
    let rec [<Inline>] DialogContent<'a> = JS.Import<'a>(nameof(DialogContent), "@fluentui/react-components")