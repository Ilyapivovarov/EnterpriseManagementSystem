namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContextSeed
{
    public static async Task InitDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<IdentityDbContextSeed>>();
        try
        {
            var context = services.GetRequiredService<IIdentityDbContext>();
            var userService = services.GetRequiredService<IUserService>();
            if (!context.Users.Any())
            {
                var defaultUser = userService.Create("admin@admin.com", "admin");
                
                context.Users.Add(defaultUser);

                await context.SaveChangesAsync();

                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, "Admin", "Admin",
                    defaultUser.Email.Address, DateTime.Now));

                var bus = services.GetRequiredService<IBus>();
                await bus.Publish(@event);
                logger.LogInformation("Send succcess");
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}