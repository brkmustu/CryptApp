var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.RegisterServices();

app.Run();
