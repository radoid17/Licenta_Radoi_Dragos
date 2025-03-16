using PixelForge.Data;
using PixelForge.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("PixelForge");
builder.Services.AddSqlite<PixelForgeContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
