using System;
using Microsoft.EntityFrameworkCore;
using PixelForge.Data;
using PixelForge.Dtos;
using PixelForge.Entities;
using PixelForge.Mapping;

namespace PixelForge.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame"; 

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", (PixelForgeContext dbContext) => 
            dbContext.Games
                     .Include(game => game.Genre)
                     .Select(game => game.ToGameSummaryDto())
                     .AsNoTracking());

        group.MapGet("/{id}", (int id, PixelForgeContext dbContext) => {

            Game? game = dbContext.Games.Find(id);

            return game is null ?Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame, PixelForgeContext dbContext) => {
            
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game.ToGameDetailsDto());
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, PixelForgeContext dbContext) => {
            var existingGame = dbContext.Games.Find(id);

            if(existingGame is null){
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, PixelForgeContext dbContext) => {
            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
