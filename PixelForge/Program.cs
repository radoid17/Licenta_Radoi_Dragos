using System.Runtime.CompilerServices;
using PixelForge.Dtos;
using PixelForge.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGamesEndpoints();

app.Run();
