using Microsoft.Extensions.Options;

namespace Config;

public class TestController
{
    private readonly IOptionsSnapshot<Configuration> optConfig;

    public TestController(IOptionsSnapshot<Configuration> optConfig)
    {
        this.optConfig = optConfig;
    }

    public void Test()
    {
        Console.WriteLine("Test");
        Console.WriteLine($"Name from config: {optConfig.Value.Age}");
    }
}