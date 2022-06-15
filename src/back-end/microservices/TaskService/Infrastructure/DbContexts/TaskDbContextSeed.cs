namespace TaskService.Infrastructure.DbContexts;

public static class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
    {
        var mediator = services.GetRequiredService<IMediator>();
        var taskDbContext = services.GetRequiredService<ITaskDbContext>();

        var defaultUser = taskDbContext.Users.FirstOrDefault();
        if (defaultUser == null)
        {
            defaultUser = new UserDbEntity
            {
                EmailAddress = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                IdentityGuid = Guid.NewGuid()
            };

            await services.GetRequiredService<IUserRepository>()
                .SaveUserDbEntityAsync(defaultUser);
        }

        await mediator.Send(new NewTaskRequest(new NewTask("Test name", "Test desc", "Test", defaultUser.Guid)));
    }
}