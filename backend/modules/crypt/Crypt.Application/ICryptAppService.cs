using Core.Shared.Application;

namespace Crypt.Application;

public interface ICryptAppService
{
    Task<Result> SubscribeEncryptAsync(string context);
    Task<Result> SubscribeDecryptAsync(string context);
}
