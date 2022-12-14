using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/crypt", () => "Hello World from crypt service");

        app.MapPost("/api/crypt/decrypt", async (CryptDto dto, IPublishEndpoint _publishEndpoint) =>
        {
            await _publishEndpoint.Publish(new DecryptEvent
            {
                Context = dto.Context
            });
        });

        app.MapPost("/api/crypt/encrypt", async (CryptDto dto, IPublishEndpoint _publishEndpoint) =>
        {
            await _publishEndpoint.Publish(new EncryptEvent
            {
                Context = dto.Context
            });
        });

        return app;
    }
}
