using MessageQueue.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crypt.Application;

public static class CryptAppBootstrapper
{
    public static IServiceCollection AddCryptApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICryptAppService, CryptAppService>();
        services.RegisterQueueServices(configuration);

        return services;
    }
}
