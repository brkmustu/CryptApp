using Microsoft.Extensions.DependencyInjection;

namespace Crypt.Application;

public static class CryptAppBootstrapper
{
    public static IServiceCollection AddCryptApp(this IServiceCollection services)
    {
        services.AddTransient<ICryptAppService, CryptAppService>();

        return services;
    }
}
