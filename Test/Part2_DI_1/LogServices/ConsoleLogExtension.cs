using LogServices;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConsoleLogExtension
{
    public static void AddConsoleLog(this IServiceCollection services)
    {
        services.AddScoped<ILogProvider, ConsoleLogProvider>();
    }
}