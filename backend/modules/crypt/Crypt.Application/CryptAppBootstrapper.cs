using Core.Shared;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Crypt.Application;

public static class CryptAppBootstrapper
{
    public static IServiceCollection AddCryptApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICryptAppService, CryptAppService>();
        services.RegisterQueueServices(configuration);

        return services;
    }

    public static IServiceCollection RegisterQueueServices(this IServiceCollection services, IConfiguration configuration)
    {
        var queueOptions = CommonSettings.GetRabbitMqSettings(configuration);

        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(queueOptions.HostName, queueOptions.VirtualHost,
                h =>
                {
                    h.Username(queueOptions.UserName);
                    h.Password(queueOptions.Password);
                });

            cfg.ExchangeType = ExchangeType.Direct;
        }));

        services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

        return services;
    }
}
