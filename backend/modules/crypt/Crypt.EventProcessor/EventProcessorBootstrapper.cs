using Crypt.Application.Consumers;
using MassTransit;

namespace Crypt.EventProcessor;

public static class EventProcessorBootstrapper
{
    public static IServiceCollection AddEventProcessorApp(this IServiceCollection services)
    {
        services.AddMassTransit(c =>
        {
            c.AddConsumer<DencryptConsumer>();
            c.AddConsumer<EncryptConsumer>();

            c.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
        });

        return services;
    }
}
