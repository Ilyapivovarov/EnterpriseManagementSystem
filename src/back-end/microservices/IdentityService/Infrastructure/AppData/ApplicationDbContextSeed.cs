using IdentityService.Core.DbEntities;

namespace IdentityService.Infrastructure.AppData;

public sealed class ApplicationDbContextSeed
{
    public static async Task SeedData(ApplicationDbContext context, ILogger logger)
    {
        try
        {
            if (await context.Database.CanConnectAsync())
                if (!context.Users.Any())
                {
                    context.Users.Add(new UserDbEntity
                    {
                        Password = "admin",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Role = new UserRoleDbEntity
                        {
                            Name = "Admin"
                        },
                        Address = new EmailAddressDbEntity
                        {
                            Email = "admin@admin.com",
                            IsVerified = true
                        }
                    });

                    await context.SaveChangesAsync();
                }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}