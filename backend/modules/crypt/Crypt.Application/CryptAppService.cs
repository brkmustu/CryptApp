using Core.Shared.Application;
using Core.Shared.Security;
using Crypt.Application.Contracts;

namespace Crypt.Application;

public class CryptAppService : ICryptAppService
{

    public async Task<Result<CryptDto>> DecryptAsync(CryptDto context)
    {
        return await Task.FromResult(Result.Success(new CryptDto
        {
            Context = SimpleStringCipher.Instance.Decrypt(context.Context)
        }));
    }

    public async Task<Result<CryptDto>> EncryptAsync(CryptDto context)
    {
        return await Task.FromResult(Result.Success(new CryptDto
        {
            Context = SimpleStringCipher.Instance.Encrypt(context.Context)
        }));
    }
}
