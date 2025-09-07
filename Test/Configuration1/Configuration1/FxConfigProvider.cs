using System.Xml;
using Microsoft.Extensions.Configuration;

namespace Configuration1;


class FxConfigProvider : FileConfigurationProvider
{
    public FxConfigProvider(FxConfigSource src) : base(src)
    {
    }
    
    
    public override void Load(Stream stream)
    {
        var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(stream);
        var csNodes = xmlDoc.SelectNodes("/configuration/connectionStrings/add");
        foreach (XmlNode xmlNode in csNodes.Cast<XmlNode>())
        {
            string name = xmlNode.Attributes["name"].Value;
            string connectionString = xmlNode.Attributes["connectionString"].Value;

            // [conn1:{connectionString: "fafdsfa", providerName: "mysql"}, ...]
            // conectionString: "3333", providerName: "mysql"
            data[$"{name}:connectionString"] = connectionString;

            var attProviderName = xmlNode.Attributes["providerName"];
            if (attProviderName != null)
            {
                data[$"{name}:providerName"] = attProviderName.Value;
            }

        }

        var asNodes = xmlDoc.SelectNodes("/configuration/appSettings/add");
        foreach (XmlNode xmlNode in asNodes.Cast<XmlNode>())
        {
            string key = xmlNode.Attributes["key"].Value;
            key = key.Replace(".", ":"); // Replace dots with colons for hierarchical keys
            string value = xmlNode.Attributes["value"].Value;

            data[key] = value;

        }
        
        this.Data = data;
    }
}
    
