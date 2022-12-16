using Crypt.Application;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Crypt.EventProcessor.Consumers;

public class EncryptConsumer : IConsumer<EncryptEvent>
{
    private readonly ILogger<EncryptConsumer> _logger;
    private readonly IHubContext<CryptionHub, ICryptionHub> _hub;
    private readonly ICryptAppService _service;

    public EncryptConsumer(
            ILogger<EncryptConsumer> logger,
            IHubContext<CryptionHub, ICryptionHub> hub,
            ICryptAppService service
        )
    {
        _logger = logger;
        _hub = hub;
        _service = service;
    }

    public async Task Consume(ConsumeContext<EncryptEvent> context)
    {
        try
        {
            var result = await _service.EncryptAsync(new CryptDto
            {
                Context = context.Message.Context
            });

            await _hub.Clients.All.Encrypt(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("EncryptConsumer error", ex);
        }
    }
}
