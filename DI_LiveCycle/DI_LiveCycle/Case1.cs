namespace DI_LiveCycle;

public class Case1
{
    
}


public class Controller
{
    private readonly ILog log;
    private readonly IStorage storage;

    public Controller(ILog log, IStorage storage)
    {
        this.log = log;
        this.storage = storage;
    }
    
    public void Test()
    {
        this.log.Log("Test message");
        this.storage.Save("Hello World", "test.txt");
    }
}

public interface ILog
{
    public void Log(string msg);
}

public classLogImpl : ILog
{
    public void Log(string msg)
    {
        Console.WriteLine(msg);
    }
}

interface IConfig
{
    public string GetValue(string name);
}    

public class ConfigImpl : IConfig
{
    public string GetValue(string name)
    {
        return "value";
    }
}

public interface IStorage
{
    public void Save(string content, string name);
}

public class StorageImpl : IStorage
{
    private readonly IConfig config;

    public StorageImpl(IConfig config)
    {
        this.config = config;
    }
    
    public void Save(string content, string name)
    {
        string server = config.GetValue("server");
        Console.WriteLine($"server: {server}, content: {content}, name: {name}");
    }
}