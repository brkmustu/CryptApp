using Core.Shared.Security;
using Crypt.Application;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Crypt.EventProcessor.Consumers;

public class DecryptConsumer : IConsumer<DecryptEvent>
{
    private readonly ILogger<DecryptConsumer> _logger;
    private readonly IHubContext<CryptionHub, ICryptionHub> _hub;
    private readonly ICryptAppService _service;

    public DecryptConsumer(
            ILogger<DecryptConsumer> logger, 
            IHubContext<CryptionHub, ICryptionHub> hub, 
            ICryptAppService service
        )
    {
        _logger = logger;
        _hub = hub;
        _service = service;
    }

    public async Task Consume(ConsumeContext<DecryptEvent> context)
    {
        try
        {
            var result = await _service.DecryptAsync(new CryptDto
            {
                Context = context.Message.Context
            });

            await _hub.Clients.All.Decrypt(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("DencryptConsumer error", ex);
        }
    }
}
