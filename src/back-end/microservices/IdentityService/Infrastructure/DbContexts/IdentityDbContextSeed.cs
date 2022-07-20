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
                
                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, "Admin", "Admin",
                    defaultUser.Email.Address, DateTime.Now));

                var bus = services.GetRequiredService<IBus>();
                var endPoint = await bus.GetPublishSendEndpoint<SignUpUserIntegrationEvent>();
                await endPoint.Send(@event);
                // await context.SaveChangesAsync();
                
                logger.LogInformation("Send succcess");
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An error occurred while migrating or seeding the database");
        }
    }
}