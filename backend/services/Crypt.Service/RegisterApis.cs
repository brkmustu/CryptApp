
using Crypt.Application;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/crypt", () => "Hello World from crypt service");

        app.MapPost("/api/crypt/decrypt", async (string context, ICryptAppService service) => await service.SubscribeDecryptAsync(context));

        app.MapPost("/api/crypt/encrypt", async (string context, ICryptAppService service) => await service.SubscribeEncryptAsync(context));

        return app;
    }
}
