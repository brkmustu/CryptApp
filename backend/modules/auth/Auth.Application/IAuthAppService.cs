using Auth.Application.Contracts;
using Core.Shared.Application;

namespace Auth.Application;

public interface IAuthAppService
{
    Task<Result<TokenResult>> SignInAsync(SignInRequestDto request);
    Task<Result<bool>> ValidateAsync(string token);
}
