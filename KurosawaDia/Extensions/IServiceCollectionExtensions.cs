using Config.Net;
using KurosawaDia.Events;
using KurosawaDia.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KurosawaDia.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddConfig(this IServiceCollection services)
    {
        var appSettings = new ConfigurationBuilder<IBotConfigService>()
            .UseEnvironmentVariables()
            .UseJsonFile("appsettings.json")
            .Build();

        services.AddSingleton<IBotConfigService>(appSettings);
    }


    public static void AddEvents(this IServiceCollection services)
    {
        services.AddTransient<IAutoStartService, LogEvent>();
        services.AddTransient<IAutoStartService, ReadEvent>();
    }
}
