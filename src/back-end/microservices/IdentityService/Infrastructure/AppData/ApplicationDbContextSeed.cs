using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.AppData;

public class ApplicationDbContextSeed
{
    public static async void SeedData(ApplicationDbContext context, ILogger logger)
    {
        try
        {
            logger.LogInformation($"{context.Database.GetConnectionString()}");
            if (await context.Database.CanConnectAsync())
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(new User()
                    {
                        Email = "admin@admin.com",
                        Password = "admin",
                        FirstName = "Admin",
                        LastName = "Admin"
                    });
                    
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical("An error occurred while migrating or seeding the database", ex);
        }
    }
}