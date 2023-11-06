open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open WebSharper.AspNetCore
open WsReactExample.Shared


[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    
    // Add services to the container.
    builder.Services
        .AddWebSharper()
        .AddWebSharperRemoting<IApi>(Service.SharedApi)
    |> ignore

    let app = builder.Build()

    // Configure the HTTP request pipeline.
    if not (app.Environment.IsDevelopment()) then
        app.UseExceptionHandler("/Error")
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            .UseHsts()
        |> ignore

    WebSharper.Web.Remoting.DisableCsrfProtection ()
    WebSharper.Web.Remoting.AddAllowedOrigin "*"

    app
        // .UseHttpsRedirection()
        .UseWebSharper(fun bld -> 
            bld
                .UseRemoting(true)
                .Sitelet(Service.Main)
            |> ignore)
        // .UseWebSharperSitelets(fun bld -> bld.Sitelet(Service.Main) |> ignore)
        // legalább lássam hogy mi volt a terv ezzel
    |> ignore
    
    app.Run()

    0 // Exit code
