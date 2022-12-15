using Core.Shared.Application;
using Crypt.Application.Contracts;

namespace Crypt.Application;

public class CryptAppService : ICryptAppService
{

    public Task<Result<CryptDto>> DecryptAsync(CryptDto context)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CryptDto>> EncryptAsync(CryptDto context)
    {
        throw new NotImplementedException();
    }
}
