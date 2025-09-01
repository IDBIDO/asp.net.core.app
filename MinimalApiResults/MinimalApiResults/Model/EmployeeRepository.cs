namespace MinimalApiResults.Model;

public static class EmployeeRepository
{
    private static List<Employee> _employees = new()
    {
        new Employee { Id = 1, Name = "Alice", Position = "Developer" },
        new Employee { Id = 2, Name = "Bob", Position = "Designer" },
        new Employee { Id = 3, Name = "Charlie", Position = "Manager" }
    };

    public static IEnumerable<Employee> GetAll() => _employees;

    public static List<Employee> GetEmployees() => _employees;
    
    public static Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public static void Add(Employee employee) => _employees.Add(employee);

    public static void Update(Employee employee)
    {
        var existingEmployee = GetById(employee.Id);
    }
}