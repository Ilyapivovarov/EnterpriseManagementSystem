namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContextSeed
{
    public static void InitData(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<IdentityDbContextSeed>>();
        try
        {
            var context = services.GetRequiredService<IIdentityDbContext>();
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

                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}