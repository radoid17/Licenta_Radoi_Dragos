using Microsoft.EntityFrameworkCore;

namespace PixelForge.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PixelForgeContext>();
        dbContext.Database.Migrate();
    }
}
