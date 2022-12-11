using Auth.Application.Contracts;
using Auth.Application;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/auth", () => "Hello World from auth service");

        app.MapPost("/api/auth/signin", async (SignInRequestDto request, IAuthAppService service) => await service.SignInAsync(request));

        app.MapPost("/api/auth/validate", async (string token, IAuthAppService service) => await service.ValidateAsync(token));

        return app;
    }
}
