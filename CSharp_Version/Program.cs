using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

app.MapOpenApi();
app.MapGet("/test1", () => "Hello World!").Produces<Test1Result>(200);
app.MapGet("/test2", () => "Hello World!").Produces<Animal>(200);

app.Run();

[JsonDerivedType(typeof(Dog))]
[JsonDerivedType(typeof(Cat))]
public class Animal
{
    public string name { get; set; }
}

public class Dog : Animal
{
    public string[] favoriteFoods = [];
}

public class Cat : Animal
{
    public string favoriteFood = "tuna";
}

public class Test1Result
{
    public Dog dog;
    public Animal animal;
}