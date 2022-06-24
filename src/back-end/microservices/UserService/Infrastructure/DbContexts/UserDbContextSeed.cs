using UserService.Core.DbEntities;

namespace UserService.Infrastructure.DbContexts;

public class UserDbContextSeed
{
    public static async Task InitDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<UserDbContextSeed>>();
        try
        {
            var userDbContext = services.GetRequiredService<IUserDbContext>();
            if (!userDbContext.Users.Any())
            {
                userDbContext.Users.Add(new UserDbEntity
                {
                    EmailAddress = "admin@admin.com",
                    DateBrith = DateTime.Today,
                    FirstName = "Admin",
                    LastName = "Admin"
                });

                await userDbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
    }
}