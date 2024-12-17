open System
open System.Text.Json.Serialization
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http

[<JsonDerivedType(typeof<Dog>)>]
[<JsonDerivedType(typeof<Cat>)>]
type Animal() =
    member val name = "" with get, set

and Cat() =
    inherit Animal()
    member val favoriteFood: string = "tuna" with get, set

and Dog() =
    inherit Animal()
    member val favoriteFoods: string[] = [||] with get, set

type ResultRecord = { dog: Dog; animal: Animal }

[<EntryPoint>]
let main args =

    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddOpenApi() |> ignore

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.MapOpenApi() |> ignore

    app.MapGet("/test1", Func<string>(fun () -> "Hello World!"))
    |> _.Produces<ResultRecord>(StatusCodes.Status200OK, null, [||])
    |> ignore

    app.MapGet("/test2", Func<string>(fun () -> "Hello World!"))
    |> _.Produces<Animal>(StatusCodes.Status200OK, null, [||])
    |> ignore

    app.Run()

    0
