using Microsoft.Extensions.Configuration;

namespace Configuration1;

// Provide param
public class FxConfigSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new FxConfigProvider(this);
    }
}