namespace ConfigServices;

public class LayeredConfigReader : IConfigReader
{
    private readonly IEnumerable<IConfigServices> services;

    public LayeredConfigReader(IEnumerable<IConfigServices> services)
    {
        this.services = services;
    }
    
    public string GetValue(string name)
    {
        string value = null;
        foreach (var service in services)
        {
            string newValue = service.GetValue(name);
            if (newValue != null)
            {
                value = newValue;   // last one wins
            }
        }

        return value;
    }
}