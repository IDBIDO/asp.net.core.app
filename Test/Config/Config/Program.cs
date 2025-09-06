// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
IConfigurationRoot configurationRoot = configBuilder.Build();

// Register IConfiguration
services.AddOptions().Configure<Configuration>(e => configurationRoot.Bind(e)); // Bind configuration to POCO

// Register IConfiguration so it can be injected
services.AddScoped<TestController>();

using (var sp = services.BuildServiceProvider())
{
    // Resolve and use TestController
    var controller = sp.GetRequiredService<TestController>();
    controller.Test();
}


/*
string name = configurationRoot["name"];
Console.WriteLine($"Name from config: {name}");
string add = configurationRoot.GetSection("proxy:address").Value;
Console.WriteLine($"Proxy Address from config: {add}");


Proxy proxy = configurationRoot.GetSection("proxy").Get<Proxy>();
Console.WriteLine("Proxy configuration loaded");
Console.WriteLine($"Address: {proxy.Address} Port: {proxy.Port}");

class Proxy
{
    public string Address { get; set; }
    public int Port { get; set; }
}