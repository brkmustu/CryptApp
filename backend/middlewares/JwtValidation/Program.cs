using Auth;
using JwtValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();
});

builder.Services.AddServices();
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.SectionName));

var app = builder.Build();

app.RegisterServices();

app.Run();
