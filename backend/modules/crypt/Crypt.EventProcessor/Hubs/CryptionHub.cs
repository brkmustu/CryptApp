using Crypt.Application.Contracts;
using Microsoft.AspNetCore.SignalR;

public class CryptionHub : Hub<ICryptionHub>
{
    public async Task Decrypt(CryptDto dto)
    {
        await Clients.Others.Decrypt(dto);
    }

    public async Task Encrypt(CryptDto dto)
    {
        await Clients.Others.Encrypt(dto);
    }
}
