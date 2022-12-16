using Core.Shared.Application;
using Crypt.Application.Contracts;

public interface ICryptionHub
{
    Task Encrypt(Result<CryptDto> dto);
    Task Decrypt(Result<CryptDto> dto);
}