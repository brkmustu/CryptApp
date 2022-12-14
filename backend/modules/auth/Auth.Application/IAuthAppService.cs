using Auth.Application.Contracts;
using Core.Shared.Application;

namespace Auth.Application;

public interface IAuthAppService
{
    Task<Result<ApplicationIdentifier>> RegisterAsync(RegisterRequestDto request);
    Task<Result<TokenResult>> SignInAsync(SignInRequestDto request);
}
