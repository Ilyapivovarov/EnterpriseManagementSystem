using UserService.Core;

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
                var defaultUser = userService.Create(EmailAddress.Parse("admin@admin.com"), Password.Parse("admin"), adminRole);
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
        var adminRole = await services.GetRequiredService<IUserRoleService>()
            .GetOrCreateAndReturn(DefaultUserRoleNames.Admin);

        var readerRole = await services.GetRequiredService<IUserRoleService>()
            .GetOrCreateAndReturn(DefaultUserRoleNames.Reader);

        return !adminRole.HasError && !readerRole.HasError;
    }
    
    public static async Task InitDevDataAsync(IServiceProvider services)
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
                    throw new Exception("Admin role is null");

                var userService = services.GetRequiredService<IUserService>();
                var defaultUser = userService.Create(EmailAddress.Parse("admin@ems.com"), Password.Parse("admin"), adminRole);
                var saveUserResult = await services.GetRequiredService<IUserRepository>()
                    .SaveUserAsync(defaultUser);
                if (!saveUserResult)
                    throw new Exception("Error while save default user");

                var @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, "Admin", "Admin",
                    defaultUser.Email.Address, DateTime.Now));

                var bus = services.GetRequiredService<IBus>();
                var endPoint = await bus.GetPublishSendEndpoint<SignUpUserIntegrationEvent>();
                await endPoint.Send(@event);
                
                var readerRole = await services.GetRequiredService<IUserRoleRepository>().GetReaderRole();
                if (readerRole == null)
                    throw new Exception("Reader role is null");

                for (var i = 1; i < 10; i++)
                {
                    var name = $"Test{1}";
                    var testUser = userService.Create(EmailAddress.Parse($"{name}@ems.com"), Password.Parse($"{name}@ems.com"), readerRole);
                    var saveResult = await services.GetRequiredService<IUserRepository>()
                        .SaveUserAsync(testUser);
                    if (!saveResult)
                        throw new Exception("Error while save test user");

                    @event = new SignUpUserIntegrationEvent(new UserDataResponse(defaultUser.Guid, name, name,
                        testUser.Email.Address, DateTime.Now));

                    bus = services.GetRequiredService<IBus>();
                    endPoint = await bus.GetPublishSendEndpoint<SignUpUserIntegrationEvent>();
                    
                    await endPoint.Send(@event);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, ex.Message);
        }
    }
}