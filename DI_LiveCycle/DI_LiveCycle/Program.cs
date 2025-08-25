// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection();
// services.AddTransient<TestServiceImpl>();
// services.AddSingleton<TestServiceImpl>();
// services.AddScoped<TestServiceImpl>();

services.AddScoped<ITestService, TestServiceImpl>();
using (ServiceProvider sp = services.BuildServiceProvider())
{
    /*
    TestServiceImpl t = sp.GetService<TestServiceImpl>();
    t.Name = "Test";
    t.SayHi();
    
    TestServiceImpl t2 = sp.GetService<TestServiceImpl>();
    t2.Name = "Test2";
    bool sameInstance = ReferenceEquals(t, t2);
    t2.SayHi();
    t.SayHi();
    Console.WriteLine($"Are t and t2 the same instance? {sameInstance}");
    */

    ITestService aux;

    using (IServiceScope scope = sp.CreateScope())
    {
        ITestService t = sp.GetService<ITestService>();
        t.Name = "Test";
        t.SayHi();
    
        ITestService t2 = sp.GetService<ITestService>();
        t2.Name = "Test2";
        bool sameInstance = ReferenceEquals(t, t2);
        t2.SayHi();
        t.SayHi();
        Console.WriteLine($"Are t and t2 the same instance? {sameInstance}");
        aux = t2; // Keep a reference to t2 for later use
    }
    
    using (IServiceScope scope2 = sp.CreateScope())
    {
        ITestService t3 = scope2.ServiceProvider.GetService<ITestService>();
        //ITestService t3 = sp.GetRequiredService<ITestService>();
        t3.Name = "Test3";
        t3.SayHi();
        
        aux.SayHi();
        bool sameInstance2 = ReferenceEquals(t3, aux);
        t3.SayHi();
        Console.WriteLine($"Are t3 and t2 the same instance? {sameInstance2}");
        Console.WriteLine(t3.GetType());
    }

    
}



public class TestServiceImpl : IDisposable, ITestService
{
    public string? Name { get; set; }

    public void SayHi()
    {
        Console.WriteLine($"Hi, {Name ?? "there"}!");
    }

    public void Dispose()
    {
        Console.WriteLine($"Disposing {Name ?? "TestServiceImpl"}");
    }
}

public interface ITestService
{
    string? Name { get; set; }
    void SayHi();
}