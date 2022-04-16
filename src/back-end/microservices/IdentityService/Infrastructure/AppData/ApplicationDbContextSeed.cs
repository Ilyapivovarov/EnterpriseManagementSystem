namespace IdentityService.Infrastructure.AppData;

public class ApplicationDbContextSeed
{
    public static async Task SeedData(ApplicationDbContext context, ILogger logger)
    {
        try
        {
            if (await context.Database.CanConnectAsync())
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(new User()
                    {
                        Email = "admin@admin.com",
                        Password = "admin",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Role = UserRole.Admin
                    });

                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}