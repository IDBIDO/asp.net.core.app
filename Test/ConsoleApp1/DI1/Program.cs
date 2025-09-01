// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

ServiceCollection service = new ServiceCollection();
//service.AddTransient<TestServiceImpl>();
service.AddSingleton<TestServiceImpl>();

using (ServiceProvider sp = service.BuildServiceProvider())
{
    TestServiceImpl t = sp.GetRequiredService<TestServiceImpl>();
    t.Name = "Test Service";
    t.SayHi();
    
    TestServiceImpl t2 = sp.GetRequiredService<TestServiceImpl>();
    Console.WriteLine(object.ReferenceEquals(t, t2)); // Should print 'False' since it's transient
}


public class TestServiceImpl
{
    public string Name { get; set; } = "";

    public void SayHi()
    {
        Console.WriteLine($"Hello from {Name}!");
    }
}