using ConfigServices;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

public static class IniFileConfigExtension
{
    public static void AddIniFileConfig(this IServiceCollection services, string filePath)
    {
        services.AddSingleton<IConfigServices> (s=> new IniFileConfigServices{FilePath=filePath});
    }
}