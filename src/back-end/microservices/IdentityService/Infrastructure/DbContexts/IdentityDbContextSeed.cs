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
                    EmailAddress = new EmailAddressDbEntity
                    {
                        Address = "admin@admin.com",
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

    public static async Task InitDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<IdentityDbContextSeed>>();
        try
        {
            var context = services.GetRequiredService<IIdentityDbContext>();
            var bus = services.GetRequiredService<IBus>();
            if (!context.Users.Any())
            {
                var defaultUser = new UserDbEntity
                {
                    Password = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Role = new UserRoleDbEntity
                    {
                        Name = "Admin"
                    },
                    EmailAddress = new EmailAddressDbEntity
                    {
                        Address = "admin@admin.com",
                        IsVerified = true
                    }
                };

                var @event = new SignUpUserIntegrationEvent(defaultUser.ToDto());
                await bus.Publish(@event);

                context.Users.Add(defaultUser);

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}