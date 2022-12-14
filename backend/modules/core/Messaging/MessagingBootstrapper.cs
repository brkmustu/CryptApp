using Core.Shared.RabbitMq;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging;

public static class MessagingBootstrapper
{
    public static IServiceCollection AddMqProducerSide(this IServiceCollection services, IConfiguration configuration)
    {
        var options = RabbitMqOptions.GetOptions(configuration);

        services.AddMassTransit(c =>
        {
            c.UsingRabbitMq((context, config) =>
            {
                config.Host(options.HostName, options.VirtualHost, hc =>
                {
                    hc.Username(options.UserName);
                    hc.Password(options.Password);
                });
            });
        });

        services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
        services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

        return services;
    }

    public static IServiceCollection AddMqConsumerSide(
            this IServiceCollection services, 
            IConfiguration configuration, 
            Action<IBusRegistrationConfigurator> busConfigurator,
            MassTransitHostOptions? massTransitHostOptions = null
        )
    {
        var options = RabbitMqOptions.GetOptions(configuration);

        services.AddMassTransit(c =>
        {
            busConfigurator(c);
        });

        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(options.HostName, options.VirtualHost, h => {
                h.Username(options.UserName);
                h.Password(options.Password);
            });
        }));

        // seçenekler boş geldiyse varsayılan değerler kullanılsın.
        if (massTransitHostOptions is null)
        {
            massTransitHostOptions = new MassTransitHostOptions
            {
                WaitUntilStarted = true,
                StartTimeout = TimeSpan.FromSeconds(10),
                StopTimeout = TimeSpan.FromSeconds(30)
            };
        }

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = massTransitHostOptions.WaitUntilStarted;
                options.StartTimeout = massTransitHostOptions.StartTimeout;
                options.StopTimeout = massTransitHostOptions.StopTimeout;
            });

        return services;
    }
}
