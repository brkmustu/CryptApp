using Messaging;

public static class CryptServiceBootstrapper
{
    public static IServiceCollection AddCryptServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMqProducerSide(configuration);

        return services;
    }
}