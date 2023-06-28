using EnterpriseManagementSystem.Contracts.Messages;

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

                await CreateAndSaveDefaultUser(services);
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

        return true;
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

                await CreateAndSaveDefaultUser(services);

                var mediator = services.GetRequiredService<IMediator>();
                for (var i = 1; i < 10; i++)
                {
                    var name = $"Test{i}";

                    await mediator.Send(new SignUpRequest(new SignUpDtoDto(name, name,
                        EmailAddress.Parse($"{name}@ems.com"), Password.Parse(name), Password.Parse(name))));
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, ex.Message);
        }
    }

    private static async Task CreateAndSaveDefaultUser(IServiceProvider services)
    {
        var adminRole = await services.GetRequiredService<IUserRoleRepository>().GetAdminRole();
        if (adminRole == null)
            throw new Exception("Admin role is null");

        var userService = services.GetRequiredService<IUserService>();
        var defaultUser = userService.Create(EmailAddress.Parse("admin@ems.com"),
            Password.Parse(services.GetRequiredService<ISecurityService>().EncryptPasswordOrException("admin")),
            adminRole);
        var saveUserResult = await services.GetRequiredService<IUserRepository>()
            .SaveUserAsync(defaultUser);
        if (!saveUserResult)
            throw new Exception("Error while save default user");

        var @event = new SignUpUserMessage
        {
            IdentityGuid = defaultUser.Guid,
            FirstName = "Admin",
            LastName = "Admin",
            EmailAddress = defaultUser.Email.Address,
            DataBrith = DateTime.Now
        };

        var bus = services.GetRequiredService<IBus>();
        await bus.SendMessageAsync(@event);
    }
}