namespace WsReactExample.Client

open WebSharper
[<JavaScript>]
module Domain =


    type Pages =
    | [<Constant "counter">] Counter
    | [<Constant "fundraisers">] Fundraisers
    | [<Constant "settings">] Settings
    | [<Constant "showcase">] Showcase