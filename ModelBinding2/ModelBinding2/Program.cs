var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// This endpoint uses custom model binding for the Person class
app.MapGet("person", (Person? p) =>
{
    return $"Id is {p?.Id}, Name is {p?.Name}";
});

app.Run();

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public static ValueTask<Person?> BindAsync(HttpContext context)
    {
        var idStr = context.Request.Query["id"];
        var nameStr = context.Request.Query["name"];

        if (int.TryParse(idStr, out var id))
        {
            return new ValueTask<Person?>(new Person { Id = id, Name = nameStr });
        }
        
        return new ValueTask<Person?>(Task.FromResult<Person?>(null));
    }
    
}
