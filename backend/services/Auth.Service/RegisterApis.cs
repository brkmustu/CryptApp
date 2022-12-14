using Auth.Application.Contracts;
using Auth.Application;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/auth", () => "Hello World from from service");

        app.MapPost("/api/auth/signin", async (SignInRequestDto request, IAuthAppService service) => await service.SignInAsync(request));

        app.Lifetime.ApplicationStarted.Register(() =>
        {
            using (var scope = app.Services.CreateScope())
            {
                var authAppService = scope.ServiceProvider.GetService<IAuthAppService>();

                authAppService.RegisterAsync(new RegisterRequestDto
                {
                    ApiKey = "00000000-0000-0000-1111-000000000000",
                    Password = "!38bad8_92f@T7",
                });
            }
        });

        return app;
    }
}
