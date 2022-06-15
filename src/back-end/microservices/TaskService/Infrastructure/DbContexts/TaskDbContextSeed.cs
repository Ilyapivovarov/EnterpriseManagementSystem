namespace TaskService.Infrastructure.DbContexts;

public static class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
    {
        var taskDbContext = services.GetRequiredService<ITaskDbContext>();
        if (!taskDbContext.Users.Any() && !taskDbContext.Tasks.Any())
        {
            var defaultUser = new UserDbEntity
            {
                EmailAddress = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                IdentityGuid = Guid.NewGuid()
            };

            await services.GetRequiredService<IUserRepository>()
                .SaveUserDbEntityAsync(defaultUser);

            var mediator = services.GetRequiredService<IMediator>();
            var request = new NewTaskRequest(new NewTask("Test name", "Test desc", "Test", defaultUser.Guid));
            await mediator.Send(request);
        }
    }
}