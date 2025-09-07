using Microsoft.Extensions.Options;

namespace Configuration1;

public class TestWebConfig
{
    private IOptionsSnapshot<WebConfig> optWebConfig;
    public TestWebConfig(IOptionsSnapshot<WebConfig> optWebConfig)
    {
        this.optWebConfig = optWebConfig;
    }
    public void Test()
    {
        Console.WriteLine("WebConfig configuration loaded");
        WebConfig wcc = this.optWebConfig.Value;
        Console.WriteLine($"Conn1 ConnectionString: {wcc.Conn1.ConnectionString}, ProviderName: {wcc.Conn1.ProviderName}");
    }
}