namespace TaskService.Infrastructure.DbContexts;

public class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<TaskDbContextSeed>>();
        try
        {
            var taskService = services.GetRequiredService<ITaskService>();
            var registeredStatus = await taskService.GetOrCreateTaskByName("Registered");
            if (registeredStatus.Value == null)
                throw new NullReferenceException();

            await taskService.GetOrCreateTaskByName("Active");
            await taskService.GetOrCreateTaskByName("Completed");

            var taskRepository = services.GetRequiredService<ITaskRepository>();
            var tasksCount = await taskRepository.GetTasksCount();

            if (tasksCount == 0)
            {
                var taskDbContext = services.GetRequiredService<IUserRepository>();
                var firstUser = await taskDbContext.GetUserById(1);
                var tryCount = 1;
                while (tryCount < 5 && firstUser == null)
                {
                    Thread.Sleep(1000);
                    firstUser = await taskDbContext.GetUserById(1);

                    tryCount++;
                }

                if (firstUser == null)
                    throw new ArgumentNullException(nameof(firstUser));

                await taskRepository.SaveTaskAsync(new TaskDbEntity
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
