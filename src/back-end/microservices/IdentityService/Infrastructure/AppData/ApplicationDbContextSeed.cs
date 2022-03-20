using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.AppData;

public static class ApplicationDbContextSeed
{
    public static async void SeedData(IServiceProvider services)
    {
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    Email = "admin@admin.com",
                    Password = "admin",
                    FirstName = "Admin",
                    LastName = "Admin"
                });
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database");

            throw;
        }
    }
}