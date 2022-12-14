using Core.Shared.Security;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Crypt.Application.Consumers;

public class DencryptConsumer : IConsumer<DecryptEvent>
{
    private readonly ILogger<DencryptConsumer> _logger;

    public DencryptConsumer(ILogger<DencryptConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DecryptEvent> context)
    {
        try
        {
            await context.RespondAsync(new CryptDto
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
