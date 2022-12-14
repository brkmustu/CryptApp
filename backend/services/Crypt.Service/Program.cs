using Crypt.Application;
using MessageQueue.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();
});

builder.Services.AddCryptApp(builder.Configuration)
    .RegisterEventBusServices();

var app = builder.Build();

app.RegisterServices();

app.Run();
