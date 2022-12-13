var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();
});

var app = builder.Build();

app.RegisterServices();

app.Run();
