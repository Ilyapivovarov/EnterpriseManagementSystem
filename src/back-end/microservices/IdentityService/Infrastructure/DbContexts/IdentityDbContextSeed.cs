namespace IdentityService.Infrastructure.DbContexts;

public sealed class IdentityDbContextSeed
{
    public static async Task InitDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<IdentityDbContextSeed>>();
        try
        {
            var users = await services.GetRequiredService<IUserRepository>().GetUsersByPageAsync();
            if (users == null || users.Length == 0)
            {
                var saveRoleResult = await SaveDefaultRolesAsync(services);
                if (!saveRoleResult)
                    throw new Exception("Error while save user roles");

                var adminRole = await services.GetRequiredService<IUserRoleRepository>().GetAdminRole();
                if (adminRole == null)
                    throw new Exception("Default role is null");

                var userService = services.GetRequiredService<IUserService>();
                var defaultUser = userService.Create("admin@admin.com", "admin", adminRole);
                var saveUserResult = await services.GetRequiredService<IUserRepository>()
                    .SaveUserAsync(defaultUser);
                if (!saveUserResult)
                    throw new Exception("Error while save default user");

                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, "Admin", "Admin",
                    defaultUser.Email.Address, DateTime.Now));

                var bus = services.GetRequiredService<IBus>();
                var endPoint = await bus.GetPublishSendEndpoint<SignUpUserIntegrationEvent>();
                await endPoint.Send(@event);
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, ex.Message);
        }
    }

    private static async Task<bool> SaveDefaultRolesAsync(IServiceProvider services)
    {
        var adminRole = await services.GetRequiredService<IUserRoleService>().GetOrCreate("Admin");
        var readerRole = await services.GetRequiredService<IUserRoleService>().GetOrCreate("Reader");

        return await services.GetRequiredService<IUserRoleRepository>().SaveRange(adminRole, readerRole);
    }
}