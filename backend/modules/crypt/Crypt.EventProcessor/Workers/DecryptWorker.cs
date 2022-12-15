using Core.Shared.RabbitMq;
using Crypt.EventProcessor.Consumers;
using MassTransit;

public class DecryptWorker : BackgroundService
{
    private readonly IBusControl _busControl;
    private readonly IServiceProvider _serviceProvider;

    public DecryptWorker(IBusControl busControl, IServiceProvider serviceProvider)
    {
        _busControl = busControl;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var hostReceiveEndpointHandler = _busControl.ConnectReceiveEndpoint(RabbitMqOptions.Queues.Crypt.Decrypt, x =>
        {
            x.Consumer<DecryptConsumer>(_serviceProvider);
        });

        await hostReceiveEndpointHandler.Ready;
    }
}