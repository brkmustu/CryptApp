using Core.Shared.Application;
using Crypt.Application.Contracts;

namespace Crypt.Application;

public interface ICryptAppService
{
    Task<Result<CryptDto>> EncryptAsync(CryptDto context);
    Task<Result<CryptDto>> DecryptAsync(CryptDto context);
}
