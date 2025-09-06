using Microsoft.Extensions.Options;

namespace ConsoleAppMailSender;

public class TestController
{
    
    private readonly IOptionsSnapshot<Configuration> optConfig;

    public TestController(IOptionsSnapshot<Configuration> optConfig)
    {
        this.optConfig = optConfig;
    }

    public void Test()
    {
        Console.WriteLine("Proxy configuration loaded");
        Console.WriteLine($"Address: {optConfig.Value.Age}");
    }
}

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