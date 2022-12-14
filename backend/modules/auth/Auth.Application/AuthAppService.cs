using Auth.Application.Contracts;
using Caching;
using Core.Shared.Application;
using Core.Shared.Caching;
using Core.Shared.Security;
using Microsoft.Extensions.Options;

namespace Auth.Application;

public class AuthAppService : IAuthAppService
{
    private readonly ICachingService _cachingService;
    private readonly TokenOptions _tokenOptions;

    public AuthAppService(ICachingService cachingService, IOptions<TokenOptions> tokenOptions)
    {
        _cachingService = cachingService;
        _tokenOptions = tokenOptions.Value;
    }

    public async Task<Result<ApplicationIdentifier>> RegisterAsync(RegisterRequestDto request)
    {
        var cacheKey = CollectionNames.Applications + "_" + request.ApiKey;

        var currentApp = await _cachingService.GetAsync<ApplicationIdentifier>(cacheKey);

        if (currentApp != null)
        {
            return Result.Failure<ApplicationIdentifier>(
                    new string[] { $"Sistemde '{request.ApiKey}' adında uygulama bulunduğundan kayıt gerçekleştirilemedi." }
                );
        }

        var encryptedPassword = request.Password.CreatePasswordHash();

        var registeredApp = new ApplicationIdentifier
        {
            ApiKey = request.ApiKey,
            PasswordHash = encryptedPassword.PasswordHash,
            PasswordSalt = encryptedPassword.PasswordSalt
        };

        await _cachingService.SetAsync(cacheKey, registeredApp);

        return Result.Success(registeredApp);
    }

    public async Task<Result<TokenResult>> SignInAsync(SignInRequestDto request)
    {
        var cacheKey = CollectionNames.Applications + "_" + request.ApiKey;

        var app = await _cachingService.GetAsync<ApplicationIdentifier>(cacheKey);

        if (app is null)
        {
            return Result.Failure<TokenResult>(new string[] { "Api key'e ait kayıt sistemde bulunamadığından işlem gerçekleştirilemedi!" });
        }

        if (request.Password.VerifyPasswordHash(app.PasswordHash, app.PasswordSalt))    
        {
            var token = app.CreateToken(_tokenOptions);

            return Result.Success(token);
        }

        return Result.Success(new TokenResult
        {
            Token = ""
        });
    }
}
