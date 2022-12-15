using Crypt.Application.Contracts;

public interface ICryptionHub
{
    Task Encrypt(CryptDto dto);
    Task Decrypt(CryptDto dto);
}