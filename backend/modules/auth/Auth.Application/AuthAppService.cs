using Auth.Application.Contracts;
using Core.Shared.Application;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Application;

public class AuthAppService : IAuthAppService
{
    private readonly IConnectionMultiplexer _redis;

    public AuthAppService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<Result<TokenResult>> SignInAsync(SignInRequestDto request)
    {
        return Result.Success(new TokenResult
        {
            Token = ""
        });
    }

    public Task<Result<bool>> ValidateAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        throw new NotImplementedException();
    }
}
