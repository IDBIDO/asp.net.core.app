namespace ConfigServices;

public interface IConfigReader
{
    public string GetValue(string name);
}