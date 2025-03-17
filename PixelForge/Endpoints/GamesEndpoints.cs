using System;
using PixelForge.Data;
using PixelForge.Dtos;
using PixelForge.Entities;
using PixelForge.Mapping;

namespace PixelForge.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame"; 

    private static readonly List<GameDto> games = [
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
            new DateOnly(2012, 10, 30)),
        
        new (
            3,
            "Half Life 2",
            "First PErson Shooter",
            9.99M,
            new DateOnly(2004, 11, 16))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) => {

            GameDto? game = games.Find(game => game.Id ==  id);

            return game is null ?Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame, PixelForgeContext dbContext) => {
            
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game.ToDto());
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => game.Id == id);

            if(index == -1){
                return Results.NotFound();
            }

            games[index] =new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
