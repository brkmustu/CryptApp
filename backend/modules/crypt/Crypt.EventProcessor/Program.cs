using Crypt.Application;
using Crypt.EventProcessor;
using Crypt.EventProcessor.Workers;
using MessageQueue.RabbitMq;
using MessageQueue.Shared.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<EncryptWorker>();
    services.Configure<RabbitMqOptions>(hostContext.Configuration.GetSection(RabbitMqOptions.SectionName));
    services.AddEventProcessorApp();
    services.AddCryptApp(builder.Configuration);
    services.RegisterEventBusServices();
});

var app = builder.Build();

app.MapGet("/", () => "Hello world from crypt event processor!");

app.Run();
