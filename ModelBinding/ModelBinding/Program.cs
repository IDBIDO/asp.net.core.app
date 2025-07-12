using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseRouting();

/********* Bind to route values *********/

// implicitly bind to a model
app.MapGet("/employees1/{id:int}", (int id) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = id, Name = "John Doe" });
});

// explicitly bind to a model
app.MapGet("/employees/{id:int}", ([FromRoute] int id) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = id, Name = "John Doe" });
});

// bind to a model with a custom name
// the attribute in route and the parameter name must match by name, by type, by requiredness
app.MapGet("/employees2/{employeeId:int}", ([FromRoute(Name = "employeeId")] int id) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = id, Name = "John Doe" });
});


/********* Bind to query string *********/

// The priority of the binding is by value from the route first, then from the query string

// adding /employees/querystring?id=1 to the URL will bind the id parameter
app.MapGet("/employees/querystring", (int id) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = id, Name = "John Doe" });
});

// in explicit binding, we can use [FromQuery] attribute
// because query string is not required by default, we should make the binding parameter nullable
app.MapGet("/employees/querystring", ([FromQuery] int? id) =>
{
    // Simulate fetching an employee by ID
    if (id == null)
    {
        return Results.BadRequest("Id is required");
    }
    
    return Results.Ok(new { Id = id, Name = "John Doe" });
});

/********* Bind to http header *********/
// We need to use [FromHeader] attribute to bind to a header
// Use postman to test this endpoint by adding a header with key "X-Employee-Id" and value "1"

app.MapGet("/employees/header", ([FromHeader(Name = "X-Employee-Id")] int id) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = id, Name = "John Doe" });
});

/********* Bind to multiple sources *********/

app.MapGet("/person/{id:int}", ([AsParameters] GetEmployeeParameter param) =>
{
    // Simulate fetching an employee by ID
    return Results.Ok(new { Id = param.Id, Name = param.Name, Position = param.Position });
});

/********* Bind to array *********/
app.MapGet("/person/array", ([FromQuery(Name="id")] int[] ids) =>
{
    // Simulate fetching employees by IDs
    return Results.Ok(ids);
});

app.MapGet("/person/header", ([FromHeader(Name="id")] int[] ids) =>
{
    // Simulate fetching employees by IDs
    return Results.Ok(ids);
});

/********* Bind to body *********/
// using complex parameter ASP.NET Core will automatically bind the body to the parameter
app.MapPost("/employee/body", (Employee employee) =>
{
    // Simulate fetching employees by IDs
    return Results.Ok(employee);
});

app.Run();

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
}

class GetEmployeeParameter
{
    [FromRoute]
    public int Id { get; set; }
    
    [FromQuery]
    public string Name { get; set; }
    
    [FromHeader]
    public string Position { get; set; }
}



