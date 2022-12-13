using Auth.Application.Contracts;
using Auth.Application;

public static class RegisterApis
{
    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/", (IHttpContextAccessor accessor) =>
        {
            //accessor.HttpContext.Request.Headers.TryGetValue("", new Microsoft.Extensions.Primitives.StringValues jwttoken);
            return "Hello World from auth service";
        });

        app.MapPost("/api/auth/signin", async (SignInRequestDto request, IAuthAppService service) => await service.SignInAsync(request));

        return app;
    }
}
