namespace ConfigServices;

public class IniFileConfigServices : IConfigServices
{
    public string FilePath { get; set; }
    
    public string GetValue(string name)
    {
        var kv = File.ReadAllLines(FilePath).Select(s => new { Name = s.Split('=')[0], Value = s.Split('=')[1] })
            .SingleOrDefault(kv => kv.Name == name);
        
        if (kv == null)
        {
            throw new Exception($"Key {name} not found in INI file {FilePath}");
        }
        
        return kv.Value;
    }
}