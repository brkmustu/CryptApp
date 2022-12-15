using Core.Shared.Security;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Crypt.EventProcessor.Consumers;

public class DecryptConsumer : IConsumer<DecryptEvent>
{
    private readonly ILogger<DecryptConsumer> _logger;
    private readonly IHubContext<CryptionHub, ICryptionHub> _hub;

    public DecryptConsumer(ILogger<DecryptConsumer> logger, IHubContext<CryptionHub, ICryptionHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<DecryptEvent> context)
    {
        try
        {
            await _hub.Clients.All.Decrypt(new CryptDto
            {
                Context = SimpleStringCipher.Instance.Decrypt(context.Message.Context)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("DencryptConsumer error", ex);
        }
    }
}
