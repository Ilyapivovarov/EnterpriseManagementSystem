namespace TaskService.Infrastructure.DbContexts;

public static class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
    {
        var taskDbContext = services.GetRequiredService<ITaskDbContext>();
        var taskRepository = services.GetRequiredService<ITaskRepository>();

        if (!taskDbContext.Tasks.Any())
        {
            var firstUser = taskDbContext.Users.First();
            await taskRepository.SaveTaskAsync(new TaskDbEntity
            {
                Author = firstUser,
                Name = "Test name",
                Executor = firstUser,
                Status = new TaskStatusDbEntity
                {
                    Name = "Registred"
                },
                Description = "Test desc"
            });

        }

    }
}
