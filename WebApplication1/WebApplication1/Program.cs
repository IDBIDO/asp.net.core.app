
// setup and create a kestrel web server

using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Generate instance of web application
var app = builder.Build();

// middleware pipeline component
//app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context) =>
    {
        // routing by location
        if (context.Request.Path == "/")
        {
            context.Response.Headers["Content-Type"] = "text/html";
            // any route will be handled by this middleware
            await context.Response.WriteAsync($"Hello World!");
            await context.Response.WriteAsync($"The method is: {context.Request.Method}<br/>");
            await context.Response.WriteAsync($"The path is: {context.Request.Path}\r\n");

            //display Headers for  
            await context.Response.WriteAsync($"The headers are: {context.Request.Headers}\r\n");
            foreach (var key in context.Request.Headers.Keys)
            {
                await context.Response.WriteAsync($"Header: {key} = {context.Request.Headers[key]}\r\n");
            }
            
            await context.Response.WriteAsync(context.Request.QueryString.ToString());
            foreach (var key in context.Request.Query.Keys)
            {
                await context.Response.WriteAsync($"Query: {key} = {context.Request.Query[key]}\r\n");
            }
        }
        else if (context.Request.Path.StartsWithSegments("/employees"))
        {
            if (context.Request.Method == "GET")
            {
                context.Response.StatusCode = 200;
                var id = context.Request.Query["id"].ToString();
                await context.Response.WriteAsync($"Employee ID: {id}\r\n");
                
                if (context.Request.Query.ContainsKey("id"))
                {
                    var employee = EmployeeRepository.GetById(int.Parse(id));
                    await context.Response.WriteAsync($"Employee ID: {employee.Id}, Name: {employee.Name}, Position: {employee.Position}\r\n");
                }
                else
                {
                    var employees = EmployeeRepository.GetAll();
                    foreach (var employee in employees)
                    {
                        await context.Response.WriteAsync(
                            $"Id: {employee.Id}, Name: {employee.Name}, Position: {employee.Position}\r\n");
                    }
                }

                // handle employees route
                //await context.Response.WriteAsync("Employees List");
            }
            else if (context.Request.Method == "POST")
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                var employee = JsonSerializer.Deserialize<Employee>(body);
                EmployeeRepository.Add(employee);
                
                context.Response.StatusCode = 201;
            }
            else if (context.Request.Method == "PUT")
            {
                    using var reader = new StreamReader(context.Request.Body);
                    var body = await reader.ReadToEndAsync();
                    var employee = JsonSerializer.Deserialize<Employee>(body);
                
                    var result = EmployeeRepository.Update(employee);
                    if (result)
                    {
                        context.Response.StatusCode = 204; // we set the status code before writing to the response
                        await context.Response.WriteAsync("Employee updated successfully.");    // this write to the response
                    }
                    else
                    {
                        context.Response.StatusCode = 404; // Not Found
                        await context.Response.WriteAsync("Employee not found.");
                        return;
                    }
            }
            else if (context.Request.Method == "DELETE")
            {
                if (context.Request.Query.ContainsKey("id"))
                {
                    var id = context.Request.Query["id"];
                    if (int.TryParse(id, out int employeeId))
                    {
                        var header = context.Request.Headers["Authorization"];

                        if (header == "he")
                        {

                            var result = EmployeeRepository.Delete(employeeId);
                            if (result)
                            {
                                await context.Response.WriteAsync("Employee deleted successfully.");
                            }
                            else
                            {
                                await context.Response.WriteAsync("Employee not fkmklmjkmound.");
                            }
                        }
                        else
                        {
                            await context.Response.WriteAsync("Unauthorized access.");
                        }
                    }
                }
                context.Response.StatusCode = 404; // Not Found
                await context.Response.WriteAsync("Employee not found.");
            }
        }
        
        else if (context.Request.Method == "DELETE")
        {
            if (context.Request.Path.StartsWithSegments("/employees"))
            {
                if (context.Request.Query.ContainsKey("id"))
                {
                    var id = context.Request.Query["id"];
                    if (int.TryParse(id, out int employeeId))
                    {
                        var header = context.Request.Headers["Authorization"];

                        if (header == "he")
                        {

                            var result = EmployeeRepository.Delete(employeeId);
                            if (result)
                            {
                                await context.Response.WriteAsync("Employee deleted successfully.");
                            }
                            else
                            {
                                await context.Response.WriteAsync("Employee not fkmklmjkmound.");
                            }
                        }
                        else
                        {
                            await context.Response.WriteAsync("Unauthorized access.");
                        }
                    }
                }
                context.Response.StatusCode = 404; // Not Found
                await context.Response.WriteAsync("Employee not found.");
                
            }
        }
    }
);


// start the kestrel web server, make it liste to incoming HTTP requests
// and convert to http context objects
// the http context objects are then passed to the middleware pipeline
app.Run();

static class EmployeeRepository
{
    private static readonly List<Employee> _employees = new()
    {
        new Employee(1, "John Doe", "Software Engineer"),
        new Employee(2, "Jane Smith", "Project Manager"),
        new Employee(3, "Sam Brown", "UX Designer")
    };

    public static IEnumerable<Employee> GetAll() => _employees;

    public static Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public static void Add(Employee? employee)
    {
        if (employee is not null)
        {
            _employees.Add(employee);
        }
    }

    public static bool Update(Employee? employee)
    {
        if (employee is not null)
        {
            var existingEmployee = GetById(employee.Id);
            if (existingEmployee is not null)
            {
                _employees.Remove(existingEmployee);
                _employees.Add(employee);
                return true;
            }
        }

        return false;
    }
    
    public static bool Delete(int id)
    {
        var employee = GetById(id);
        if (employee is not null)
        {
            _employees.Remove(employee);
            return true;
        }
        return false;
    }
}

record Employee(int Id, string Name, string Position);