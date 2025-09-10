using Microsoft.Extensions.Logging;

namespace Logging1;

public class Test1
{
    private ILogger<Test1> logger;
    
    public Test1(ILogger<Test1> logger)
    {
        this.logger = logger;
    }

    public void Test()
    {
        logger.LogDebug("Debug log");
        logger.LogInformation("Information log");
        logger.LogWarning("Warning log");
        logger.LogError("Error log");
        try
        {
            File.ReadAllText("ABC.txt");
            logger.LogTrace("Trace log");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error: {ex.Message}");
        }
        
        var user = new User { Id = 1, Name = "John" };
        logger.LogInformation("User info: {@User}", user);
    }
    
}

class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}