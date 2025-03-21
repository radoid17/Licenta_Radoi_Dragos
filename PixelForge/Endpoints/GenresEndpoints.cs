using PixelForge.Data;
using PixelForge.Mapping;
using PixelForge.Entities;
using Microsoft.EntityFrameworkCore;

namespace PixelForge.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app){
        var group = app.MapGroup("genres");

        group.MapGet("/", async (PixelForgeContext dbContext) => 
            await dbContext.Genres
                           .Select(genre => genre.ToDto())
                           .AsNoTracking()
                           .ToListAsync());
        
        return group;
    }
}
