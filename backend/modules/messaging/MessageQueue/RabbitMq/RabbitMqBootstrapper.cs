using MassTransit;
using MessageQueue.Shared;
using MessageQueue.Shared.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MessageQueue.RabbitMq;

public static class RabbitMqBootstrapper
{
    public static IServiceCollection RegisterQueueServices(
            this IServiceCollection services,
            IConfiguration configuration,
            MqExchangeType? exchangeType = null
        )
    {
        var queueOptions = MessageQueueSettings.GetRabbitMqSettings(configuration);

        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(queueOptions.HostName, queueOptions.VirtualHost,
                h =>
                {
                    h.Username(queueOptions.UserName);
                    h.Password(queueOptions.Password);
                });

            string rabbitExchangeType = ExchangeType.Direct;

            if (exchangeType is null)
            {
                rabbitExchangeType = ExchangeType.Direct;
            }
            else
            {
                switch (exchangeType)
                {
                    case MqExchangeType.Direct:
                        rabbitExchangeType = ExchangeType.Direct;
                        break;
                    case MqExchangeType.Fanout:
                        rabbitExchangeType = ExchangeType.Fanout;
                        break;
                    case MqExchangeType.Headers:
                        rabbitExchangeType = ExchangeType.Headers;
                        break;
                    case MqExchangeType.Topic:
                        rabbitExchangeType = ExchangeType.Topic;
                        break;
                    default:
                        break;
                }
            }

            cfg.ExchangeType = rabbitExchangeType;
        }));

        return services;
    }

    /// <summary>
    /// event bus olarak mass transit kullandığımızdan bu kütüphanenin servislerini ekliyoruz.
    /// bu kısım mesaj yayınlayacak olan uygulamalarda eklenmeli.
    /// bu bizim örneğimiz için "Crypt.Service" uygulaması.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterEventBusServices(this IServiceCollection services)
    {
        services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

        return services;
    }
}
