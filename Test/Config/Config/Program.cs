// See https://aka.ms/new-console-template for more information

using Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection();

ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
IConfigurationRoot configurationRoot = configBuilder.Build();

services.AddOptions()
    .Configure<Configuration>(e => configurationRoot.Bind(e))
    .Configure<Proxy>(e => configurationRoot.GetSection("proxy").Bind(e));
services.AddScoped<TestController>();
services.AddScoped<Test2>();


using (var sp = services.BuildServiceProvider())
{
    using (var scoped = sp.CreateScope())
    {
        var c2 = scoped.ServiceProvider.GetService<TestController>();
        var c = scoped.ServiceProvider.GetService<Test2>();
        c.Test();   
        c2.Test();
    }
}

    /*
string name = configurationRoot["name"];
Console.WriteLine($"Name from config: {name}");
string add = configurationRoot.GetSection("proxy:address").Value;
Console.WriteLine($"Proxy Address from config: {add}");


Proxy proxy = configurationRoot.GetSection("proxy").Get<Proxy>();
Console.WriteLine("Proxy configuration loaded");
Console.WriteLine($"Address: {proxy.Address} Port: {proxy.Port}");
*/
public class Proxy
{
    public string Address { get; set; }
    public int Port { get; set; }
}

public class Configuration
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Proxy Proxy { get; set; }
}