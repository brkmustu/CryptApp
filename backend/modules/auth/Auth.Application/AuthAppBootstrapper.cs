using Auth.Application;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Auth;

public static class AuthAppBootstrapper
{
    public static IServiceCollection AddAuthApp(this IServiceCollection services)
    {
        services.AddTransient<IAuthAppService, AuthAppService>();
        var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { "redis:6379" }
        });
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        return services;
    }
}
