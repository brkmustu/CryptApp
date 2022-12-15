using Core.Shared.RabbitMq;
using Crypt.EventProcessor.Consumers;
using MassTransit;

namespace Crypt.EventProcessor.Workers;

public class EncryptWorker : BackgroundService
{
    private readonly ILogger<EncryptWorker> _logger;
    private readonly IBusControl _busControl;
    private readonly IServiceProvider _serviceProvider;

    public EncryptWorker(IBusControl busControl, ILogger<EncryptWorker> logger, IServiceProvider serviceProvider)
    {
        _busControl = busControl;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("EncryptWorker started");

            var hostReceiveEndpointHandler = _busControl.ConnectReceiveEndpoint(RabbitMqOptions.Queues.Crypt.Encrypt, x =>
            {
                x.Consumer<EncryptConsumer>(_serviceProvider);
            });

            await hostReceiveEndpointHandler.Ready;
        }
        catch (Exception ex)
        {
            _logger.LogError("EncryptWorker error", ex);
        }
    }
}
