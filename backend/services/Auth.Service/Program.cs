using Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthApp();

var app = builder.Build();

app.RegisterServices();

app.Run();
