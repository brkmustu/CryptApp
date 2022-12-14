using Core.Shared.Security;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;

namespace Crypt.EventProcessor.Consumers;

public class DecryptConsumer : IConsumer<DecryptEvent>
{
    private readonly ILogger<DecryptConsumer> _logger;

    public DecryptConsumer(ILogger<DecryptConsumer> logger)
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
