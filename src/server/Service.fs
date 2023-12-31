module Service

open WebSharper
open WebSharper.Sitelets
open WsReactExample.Shared


let SharedApi : IApi= {
    GetValue = fun  () -> async {return "hellotext" }
    GetFundraisers = fun () -> async {
        return [|
        {
            Name="Forgot to turn off EC2 instance, help"
            FundraiserLink=None
            Owner={|
                Name="Noname"
            |}
            OwnerLink=None
            Goal=50000
        }
        {
            Name="Monyee for a Geometre dash"
            FundraiserLink=None
            Owner={|
                Name="Kimblee Geoffrey"
            |}
            OwnerLink=None
            Goal=853500
        }
    |]
    }
}
WebSharper.Core.Remoting.AddHandler typeof<IApi> SharedApi

type EndPointWithCors =
    | [<EndPoint "GET /user">] GetUser of Id: int 

type EndPoint =
    | [<EndPoint "/">] Home
    | [<EndPoint "/">] EndPointWithCors of Cors<EndPointWithCors>
    
type User =
    { 
        Id: int
        Name: string
    }

let HandleApi ctx endpoint =
    match endpoint with
    | GetUser uid ->
        Content.Json { Id = uid; Name = "John" }

[<Website>]
let Main =
    Application.MultiPage (fun ctx endpoint ->
        
        match endpoint with
        | Home -> Content.Text "Service version 1.0"
        | EndPointWithCors endpoint ->
            Content.Cors endpoint 
                (fun corsAllows ->
                    { corsAllows with
                        Origins = ["http://example.com"]
                    }
                )
                (HandleApi ctx)
    )

