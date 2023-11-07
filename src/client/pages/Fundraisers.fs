namespace WsReactExample.Client.Pages
open WebSharper
open WebSharper.JavaScript
open WebSharper.React
open WebSharper.FluentUI.React
open WsReactExample.Client.ServerCommunication
[<JavaScript>]
module FundraisersPage =

    type CompareAction<'T> = System.Func<'T,'T,int>
    type TableColumnDefinition<'TItem> = {
        columnId: string
        compare: CompareAction<'TItem>
        renderHeaderCell: unit -> React.Element
        renderCell: 'TItem -> React.Element
    }
    and 'T columnDefinition = TableColumnDefinition<'T>
    
    type TableRowData<'TItem> = {
        item: 'TItem
        rowId: string
    }
    and 'T rowData = TableRowData<'T>

    open WebSharper.React.Html

    type FundraiserInfo = {
        Name:string
        FundraiserLink: URL option
        Owner: {|
            Name:string
            Icon: React.Element option
        |}
        OwnerLink: URL option
        Goal: float
    } with
        static member FromServer(srvData: WsReactExample.Shared.FundraiserInfoServer) = {
            Name = srvData.Name
            FundraiserLink = srvData.FundraiserLink
            Owner = {|
                Name = srvData.Owner.Name
                Icon = None
            |}
            OwnerLink = srvData.OwnerLink
            Goal = srvData.Goal
        }


    let columns : (FundraiserInfo columnDefinition) array = [|
        {
            columnId="name"
            compare= (new CompareAction<_>(fun a b-> a.Name.CompareTo b.Name ))
            renderHeaderCell=(fun () -> text "Name")
            renderCell=(fun item ->
                Helpers.Table.cellLayout [] [text (sprintf $"{item.Name}")]
            )
        }
        {
            columnId="goal"
            compare= (new CompareAction<_>(fun a b -> a.Goal.CompareTo b.Goal ))
            renderHeaderCell=(fun () -> text "Goal")
            renderCell=(fun item ->
                Helpers.Table.cellLayout [] [text (sprintf $"{item.Goal}")]
            )
        }
        {
            columnId="owner"
            compare= (new CompareAction<_>(fun a b -> a.Owner.Name.CompareTo b.Owner.Name ))
            renderHeaderCell=(fun () -> text "Owner")
            renderCell=(fun item ->
                Helpers.Table.cellLayout [] [text (sprintf $"{item.Owner.Name}")]
            )
        }
    |]

    type Model = {
        Fundraisers: FundraiserInfo array
    }

    type Message =
    | FetchData
    | DataLoaded of FundraiserInfo array

    let init () = 
        {
            Fundraisers=Array.empty
        }, Elmish.Cmd.ofMsg FetchData

    let update msg model = 
        match msg with
        | FetchData ->
            let remoteMsg = 
                Elmish.Cmd.OfAsync.perform 
                    (api.GetFundraisers) 
                    () 
                    (fun res -> (res |> Array.map FundraiserInfo.FromServer) |> DataLoaded)
            model, remoteMsg
        | DataLoaded data ->
            {model with Fundraisers = data}, Elmish.Cmd.none
    module private Grid =
        let [<Inline>] renderHeader (colInfo:FundraiserInfo columnDefinition) =
            let {renderHeaderCell=renderHeaderCell}=colInfo
            JS.jsx $"<{Components.DataGridHeaderCell}>{renderHeaderCell()}</{Components.DataGridHeaderCell}>"
        let [<Inline>] renderRow (rowInfo:FundraiserInfo rowData) =
            let {item=item;rowId=rowId}=rowInfo
            JS.jsx $"""<{Components.DataGridRow}>
                {fun (colInfo: FundraiserInfo columnDefinition) ->
                    JS.jsx $"<{Components.DataGridCell}>{colInfo.renderCell(item)}</{Components.DataGridCell}>"
                }
            </{Components.DataGridRow}>"""
    let private grid model = 
        
        JS.jsx $"""<{Components.DataGrid}
                        items={model.Fundraisers}
                        columns={columns}
                        sortable
                        >
                        <{Components.DataGridHeader}>
                            <{Components.DataGridRow}>
                                {Grid.renderHeader}
                            </{Components.DataGridRow}>
                        </{Components.DataGridHeader}>
                        <{Components.DataGridBody}>
                            {Grid.renderRow}
                        </{Components.DataGridBody}>
                    </{Components.DataGrid}>
                    """
    let view model dispatch =
        div [] [
            div [
                "style", {|
                    padding="20px"
                |}
            ] [                    
                grid model
            ]
        ]
        
