using Logging1;
using Microsoft.Extensions.Logging;

namespace SystemServices;

public class Test2
{
    private ILogger<Test1> logger;
    
    public Test2(ILogger<Test1> logger)
    {
        this.logger = logger;
    }

    public void Test()
    {
        logger.LogDebug("Debug 22 log");
        logger.LogInformation("Information 22 log");
        logger.LogWarning("Warning 22 log");
        logger.LogError("Error 22 log");
    }
    
}