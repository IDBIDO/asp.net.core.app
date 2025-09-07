using Microsoft.Extensions.Configuration;

namespace Configuration1;

public static class FxConfigExtensions
{
    public static IConfigurationBuilder AddFxConfig(this IConfigurationBuilder builder, string path = null)
    {
        builder.Add(new FxConfigSource() { Path = path });
        return builder;
    }
}
