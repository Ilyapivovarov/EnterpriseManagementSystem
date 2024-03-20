namespace TaskService.Infrastructure.DbContexts;

public class TaskDbContextSeed
{
    public static async Task InitData(IServiceProvider services)
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
            await taskRepository.SaveTaskAsync(new TaskDbEntity
            {
                Author = Guid.NewGuid(),
                Name = "Test task",
                Created = DateTime.Now,
                Status = registeredStatus.Value
            });
        }
    }

    public static async Task InitDevData(IServiceProvider services)
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
            await taskRepository.SaveTaskAsync(new TaskDbEntity
            {
                Author = Guid.NewGuid(),
                Name = "Test task",
                Created = DateTime.Now,
                Status = registeredStatus.Value
            });
        }
    }
}