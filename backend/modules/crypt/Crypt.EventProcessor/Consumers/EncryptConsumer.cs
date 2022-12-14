using Core.Shared.Security;
using Crypt.Application.Contracts;
using Crypt.Events;
using MassTransit;

namespace Crypt.EventProcessor.Consumers;

public class EncryptConsumer : IConsumer<EncryptEvent>
{
    private readonly ILogger<EncryptConsumer> _logger;

    public EncryptConsumer(ILogger<EncryptConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EncryptEvent> context)
    {
        try
        {
            await context.RespondAsync(new CryptDto
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
