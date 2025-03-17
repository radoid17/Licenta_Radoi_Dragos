using Microsoft.EntityFrameworkCore;
using PixelForge.Entities;
namespace PixelForge.Data;


public class PixelForgeContext(DbContextOptions<PixelForgeContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "First Person Shooter"},
            new { Id = 2, Name = "Role Playing Game"},
            new { Id = 3, Name = "Sports"},
            new { Id = 4, Name = "Fighting"},
            new { Id = 5, Name = "Action Adventure"}
        );
    }
}
