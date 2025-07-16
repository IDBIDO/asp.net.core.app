using System.ComponentModel.DataAnnotations;
using ModelValidation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/employees", (Employee employee) =>
{
    EmployeesRepository.GetEmployees().Add(employee);
    return Results.Created($"/employees/{employee.Id}", employee);
}).WithParameterValidation();

app.Run();
public static class EmployeesRepository
{
    private static List<Employee> employees = new List<Employee> {
        new Employee(1, "John Doe", "Engineer", 60000),
        new Employee(2, "Jane Smith", "Manager", 75000),
        new Employee(3, "Sam Brown", "Technician", 50000)
    };
    
    public static List<Employee> GetEmployees() => employees;

    public static Employee? GetEmployeeById(int id)
    {
        return employees.FirstOrDefault(x => x.Id == id);
    }
}

public class Employee
{
    public int Id { get; set; }

    // C# data annotations
    [Required]
    public string Name { get; set; }

    public string Position { get; set; }

    [Required]
    [Range(5000, 200000)]
    [Employee_EnsureSalary]
    public double Salary { get; set; }

    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}