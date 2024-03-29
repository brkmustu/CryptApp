﻿using Crypt.Application;
using Crypt.EventProcessor.Consumers;
using Messaging;

namespace Crypt.EventProcessor;

public static class EventProcessorBootstrapper
{
    public static IServiceCollection AddEventProcessorApp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICryptAppService, CryptAppService>();

        services.AddMqConsumerSide(configuration, c =>
        {
            c.AddConsumer<DecryptConsumer>();
            c.AddConsumer<EncryptConsumer>();
        });

        return services;
    }
}
