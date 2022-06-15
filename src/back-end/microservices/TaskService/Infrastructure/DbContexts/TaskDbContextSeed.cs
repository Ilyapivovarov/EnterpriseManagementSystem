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

            var taskStatusDbEntity = new TaskStatusDbEntity
            {
                Name = "Registred"
            };
            await services.GetRequiredService<ITaskStatusRepository>()
                .SaveTaskStatusDbEntity(taskStatusDbEntity);
        }
    }
}