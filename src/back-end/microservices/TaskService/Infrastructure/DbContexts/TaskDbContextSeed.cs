namespace TaskService.Infrastructure.DbContexts;

public class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<TaskDbContextSeed>>();
        try
        {
            var taskDbContext = services.GetRequiredService<ITaskDbContext>();
            if (!taskDbContext.Tasks.Any())
            {
                var taskService = services.GetRequiredService<ITaskService>();

                var registeredStatus = await taskService.GetOrCreateTaskByName("Registered");
                var activeStatus = await taskService.GetOrCreateTaskByName("Active");
                var completedStatus = await taskService.GetOrCreateTaskByName("Completed");

                var firstUser = taskDbContext.Users.First();
                if (registeredStatus.Value != null)
                    await services.GetRequiredService<ITaskRepository>()
                        .SaveTaskAsync(new TaskDbEntity
                        {
                            Author = firstUser,
                            Executor = firstUser,
                            Name = "Test task",
                            Created = DateTime.Now,
                            Status = registeredStatus.Value
                        });

            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

    }
}
