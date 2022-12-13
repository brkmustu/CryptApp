using Auth.Application;
using Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Auth;

public static class AuthAppBootstrapper
{
    public static IServiceCollection AddAuthApp(this IServiceCollection services)
    {
        services.AddTransient<IAuthAppService, AuthAppService>();
        services.AddCachingModule();
        return services;
    }
}
