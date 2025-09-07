using Microsoft.Extensions.Options;

namespace Configuration1;

public class TestController
{
    private readonly IOptionsSnapshot<Config> optConfig;

    public TestController(IOptionsSnapshot<Config> optConfig)
    {
        this.optConfig = optConfig;
    }
    
    public void Test()
    {
        Console.WriteLine("Config configuration loaded");
        Console.WriteLine($"Name: {optConfig.Value.Name}, Age: {optConfig.Value.Age}");
        Console.WriteLine($"Proxy Address: {optConfig.Value.Proxy.Address}, Port: {optConfig.Value.Proxy.Port}");
    }
    
}