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
        _redis.GetDatabase();
        //var applications = await _redis.GetCollectionAsync<ApplicationIdentifier>(CollectionNames.Applications);



        return Result.Success(new TokenResult
        {
            Token = ""
        });
    }
}
