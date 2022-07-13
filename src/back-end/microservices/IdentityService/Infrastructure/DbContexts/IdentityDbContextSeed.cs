using EnterpriseManagementSystem.Contracts.WebContracts.Response;

namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContextSeed
{
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
                    Role = new UserRoleDbEntity
                    {
                        Name = "Admin"
                    },
                    Email = new EmailDbEntity
                    {
                        Address = "admin@admin.com",
                        IsVerified = true
                    }
                };
                
                context.Users.Add(defaultUser);

                await context.SaveChangesAsync();

                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, "Admin", "Admin",
                    defaultUser.Email.Address, DateTime.Now));
                await bus.Publish(@event);
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}