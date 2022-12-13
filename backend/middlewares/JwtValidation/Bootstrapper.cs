using Auth;
using Core.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtValidation;

public static class Bootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { CommonSettings.GetRedisServiceName() }
        });
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddHttpContextAccessor();
        return services;
    }

    public static WebApplication RegisterServices(this WebApplication app)
    {
        app.MapGet("/", (IConnectionMultiplexer redis, IHttpContextAccessor accessor, IOptions<TokenOptions> tokenOptions) =>
        {
            if (accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authHeader))
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    var token = authHeader.ToString().Split(' ')[1];

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.SecurityKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var appId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;


                }
                catch
                {
                    accessor.HttpContext.Response.StatusCode = 401;
                    return "Geçersiz token.";
                }
            }
            accessor.HttpContext.Response.StatusCode = 401;
            return "Yetkiniz olmadığı için işlem gerçekleştirilememiştir.";
        });

        return app;
    }
}
