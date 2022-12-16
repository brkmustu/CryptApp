using Auth.Application.Contracts;
using Auth.Application;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/api/auth", () => "Hello World from from service");

        app.MapPost("/api/auth/signin", async (SignInRequestDto request, IAuthAppService service) => await service.SignInAsync(request));

        /// uygulama ilk ayağa kalktığında, eğer redis içinde yoksa aşağıdaki apikey ve şifre ile örnek bir kayıt oluşturulur.
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            using (var scope = app.Services.CreateScope())
            {
                var authAppService = scope.ServiceProvider.GetService<IAuthAppService>();

                authAppService.RegisterAsync(new RegisterRequestDto
                {
                    ApiKey = "UdqoDma93upHRNm2rn0u",
                    Password = "123456",
                });
            }
        });

        return app;
    }
}
