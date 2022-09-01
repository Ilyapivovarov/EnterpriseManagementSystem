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
                    UserDbEntity = new UserDbEntity
                    {
                        EmailAddress = EmailAddress.Parse("admin@admin.com"),
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
    
    public static async Task InitDevDataAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<UserDbContextSeed>>();
        try
        {
            var userDbContext = services.GetRequiredService<IUserDbContext>();
            if (!userDbContext.Eployees.Any())
            {
                userDbContext.Eployees.Add(new EmployeeDbEntity
                {
                    UserDbEntity = new UserDbEntity
                    {
                        EmailAddress = EmailAddress.Parse("admin@admin.com"),
                        IdentityGuid = Guid.NewGuid(),
                        FirstName = "Admin",
                        LastName = "Admin"
                    }
                });
                
                for (var i = 0; i < 10; i++)
                {
                    var name = $"Test{i}";
                    userDbContext.Eployees.Add(new EmployeeDbEntity
                    {
                        UserDbEntity = new UserDbEntity
                        {
                            EmailAddress = EmailAddress.Parse($"{name}@ems.com"),
                            IdentityGuid = Guid.NewGuid(),
                            FirstName = name,
                            LastName = name
                        }
                    });
                }

                await userDbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
    }
}