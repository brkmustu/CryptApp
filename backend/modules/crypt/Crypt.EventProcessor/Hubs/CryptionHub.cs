using Core.Shared.Application;
using Crypt.Application.Contracts;
using Microsoft.AspNetCore.SignalR;

public class CryptionHub : Hub<ICryptionHub>
{
    public async Task Decrypt(Result<CryptDto> dto)
    {
        await Clients.Others.Decrypt(dto);
    }

    public async Task Encrypt(Result<CryptDto> dto)
    {
        await Clients.Others.Encrypt(dto);
    }
}
