// See https://aka.ms/new-console-template for more information

using Logging1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using SystemServices;

Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection(); // list of recipes for creating objects

// register logging services, including ILogger<T>, any class that ask for an ILogger can now receive one via DI
services.AddLogging(logBuilder =>
{
    //logBuilder.AddConsole();
    //logBuilder.SetMinimumLevel(LogLevel.Debug); // set minimum log level to Debug
    //logBuilder.AddNLog();

    var log = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .WriteTo.Console(new JsonFormatter())
        .CreateLogger();
    
    logBuilder.AddSerilog(log);



});  // extension method

// Test1 depends on ILogger<T>
services.AddScoped<Test1>();
services.AddScoped<Test2>();
using (var sp = services.BuildServiceProvider())
{
    var test1 = sp.GetService<Test1>();
    test1.Test();
    
    var test2 = sp.GetService<Test2>();
    test2.Test();
}