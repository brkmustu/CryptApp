using Crypt.Application;
using Crypt.EventProcessor;
using Crypt.EventProcessor.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<EncryptWorker>();
    services.AddHostedService<DecryptWorker>();
    services.AddLogging();
    services.AddSignalR();
    services.AddCryptApp();
    services.AddEventProcessorApp(builder.Configuration);
});

var app = builder.Build();

app.MapHub<CryptionHub>("/hubs/crypt");

app.MapGet("/", () => "Hello world from crypt event processor!");

app.Run();
