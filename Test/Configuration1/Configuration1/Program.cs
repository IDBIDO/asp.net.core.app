// See https://aka.ms/new-console-template for more information

using Configuration1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection();

ConfigurationBuilder configBuilder = new ConfigurationBuilder();
//configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
//configBuilder.AddCommandLine(args);
//configBuilder.Add(new FxConfigSource() {Path="web.config"});
configBuilder.AddFxConfig("web.config");


Console.WriteLine(args);
IConfigurationRoot configurationRoot = configBuilder.Build();

services.AddOptions().Configure<WebConfig>(e => configurationRoot.Bind(e)); // Bind configuration to POCO

//services.AddOptions().Configure<Config>(e => configurationRoot.Bind(e)); // Bind configuration to POCO

services.AddScoped<TestController>();
services.AddScoped<TestWebConfig>();

using (var sp = services.BuildServiceProvider())
{
    // Resolve and use TestController
    //var controller = sp.GetRequiredService<TestController>();
    //controller.Test();
    
    var c = sp.GetRequiredService<TestWebConfig>();
    c.Test();
}

public class Proxy
{
    public string Address { get; set; }
    public int Port { get; set; }
}

public class Config
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Proxy Proxy { get; set; }
}
