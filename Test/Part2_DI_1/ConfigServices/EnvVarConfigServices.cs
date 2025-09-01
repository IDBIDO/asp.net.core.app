namespace ConfigServices;

public class EnvVarConfigServices : IConfigServices
{
    public string GetValue(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }
}