using Crypt.Application;
using Crypt.Application.Contracts;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/crypt", () => "Hello World from crypt service");

        app.MapPost("/api/crypt/decrypt", async(CryptDto context, ICryptAppService service) => await service.SubscribeDecryptAsync(context));

        app.MapPost("/api/crypt/encrypt", async (CryptDto context, ICryptAppService service) => await service.SubscribeEncryptAsync(context));

        return app;
    }
}
