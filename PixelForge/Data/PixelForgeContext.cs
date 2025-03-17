using Microsoft.EntityFrameworkCore;
using PixelForge.Entities;
namespace PixelForge.Data;


public class PixelForgeContext(DbContextOptions<PixelForgeContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
