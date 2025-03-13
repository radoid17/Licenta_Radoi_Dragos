using System.Runtime.CompilerServices;
using PixelForge.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
        1,
        "Call of Duty: Black Ops",
        "First Person Shooter",
        29.99M,
        new DateOnly(2010, 11, 9)),
    
    new (
        2,
        "Assassin's Creed III",
        "Action-Adventure",
        39.99M,
        new DateOnly(2012, 10, 30))
];

app.MapGet("/", () => "Hello World!");

app.Run();
