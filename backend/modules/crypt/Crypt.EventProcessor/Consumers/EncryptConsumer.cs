using Core.Shared.Security;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Crypt.EventProcessor.Consumers;

public class EncryptConsumer : IConsumer<EncryptEvent>
{
    private readonly ILogger<EncryptConsumer> _logger;
    private readonly IHubContext<CryptionHub, ICryptionHub> _hub;

    public EncryptConsumer(ILogger<EncryptConsumer> logger, IHubContext<CryptionHub, ICryptionHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<EncryptEvent> context)
    {
        try
        {
            await _hub.Clients.All.Encrypt(new CryptDto
            {
                Context = SimpleStringCipher.Instance.Encrypt(context.Message.Context)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("EncryptConsumer error", ex);
        }
    }
}
