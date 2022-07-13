namespace UserService.Infrastructure.DbContexts;

public class UserDbContextSeed
{
    public static async Task InitDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<UserDbContextSeed>>();
        try
        {
            var userDbContext = services.GetRequiredService<IUserDbContext>();
            if (!userDbContext.Eployees.Any())
            {
                userDbContext.Eployees.Add(new EmployeeDbEntity
                {
                    User = new UserDbEntity
                    {
                        EmailAddress = "admin@admin.com",
                        IdentityGuid = Guid.NewGuid(),
                        FirstName = "Admin",
                        LastName = "Admin"
                    }
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