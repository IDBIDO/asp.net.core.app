using Microsoft.Extensions.Options;

namespace Config;

public class Test2
{
    private readonly IOptionsSnapshot<Proxy> optProxy;
    
    public Test2(IOptionsSnapshot<Proxy> optProxy)
    {
        this.optProxy = optProxy;
    }
    
    public void Test()
    {
        Console.WriteLine("Test2");
        Console.WriteLine($"Proxy Address from config: {optProxy.Value.Address}");
    }
}