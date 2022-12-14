using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Caching;

public static class DependencyInjection
{
    public static IServiceCollection AddCachingModule(this IServiceCollection services)
    {
        var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { CachingSettings.GetRedisServiceName() }
        });

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddSingleton<ICachingService, CachingService>();

        return services;
    }
}
