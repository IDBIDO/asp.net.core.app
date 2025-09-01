using MinimalApiResults.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/employees", () =>
{
    var employees = EmployeeRepository.GetAll();
    return Results.Ok(employees);
});

app.MapPost("/employees", (Employee employee) =>
{
    if (employee == null)
    {
        return Results.BadRequest("Employee cannot be null");
    }

    EmployeeRepository.Add(employee);
    return Results.Ok(employee);
});

app.Run();