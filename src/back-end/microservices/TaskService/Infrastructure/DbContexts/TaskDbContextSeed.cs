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
                var firstUsers = await taskDbContext.GetUsersByPage(1, 1);
                var tryCount = 1;
                while (tryCount < 5 && firstUsers?.Length == 0)
                {
                    Thread.Sleep(100);
                    firstUsers = await taskDbContext.GetUsersByPage(0, 1);

                    tryCount++;
                }

                var user = firstUsers?.FirstOrDefault() ?? new UserDbEntity
                {
                    EmailAddress = EmailAddress.Parse("admin@admin.com"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    IdentityGuid = Guid.NewGuid()
                };

                await taskRepository.SaveTaskAsync(new TaskDbEntity
                {
                    Author = user,
                    Executor = user,
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
