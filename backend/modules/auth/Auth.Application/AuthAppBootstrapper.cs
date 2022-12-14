using Auth.Application;
using Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth;

public static class AuthAppBootstrapper
{
    public static IServiceCollection AddAuthApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthAppService, AuthAppService>();
        services.AddCachingModule();
        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.SectionName));
        return services;
    }
}
