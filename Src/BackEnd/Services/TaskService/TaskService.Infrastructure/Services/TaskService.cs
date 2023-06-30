namespace TaskService.Infrastructure.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskStatusRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ILogger<TaskService> logger,
        ITaskStatusRepository taskStatusRepository)
    {
        _logger = logger;
        _taskRepository = taskStatusRepository;
    }

    public async Task<ServiceResult<TaskStatusDbEntity>> GetOrCreateTaskByName(string name)
    {
        var taskDbEntity = await _taskRepository.GetByName(name);
        if (taskDbEntity != null)
            return new ServiceResult<TaskStatusDbEntity>(taskDbEntity);

        var newTaskStatus = new TaskStatusDbEntity
        {
            Name = name
        };
        var saveResult = await _taskRepository.SaveAsync(newTaskStatus);
        if (saveResult)
            return new ServiceResult<TaskStatusDbEntity>(newTaskStatus);

        return new ServiceResult<TaskStatusDbEntity>($"Error wihle get or create satus with name {name}");
    }

    public ServiceResult<TaskDbEntity> UpdateTaskStatus(TaskStatusDbEntity taskStatusDbEntity,
        TaskDbEntity taskDbEntity)
    {
        try
        {
            taskDbEntity.Status = taskStatusDbEntity;

            return new ServiceResult<TaskDbEntity>(taskDbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while updating task status");
        }
    }

    public ServiceResult<TaskDbEntity> SetExecutor(TaskDbEntity task, Guid? newExecutor)
    {
        try
        {
            if (task.Executor != newExecutor)
                task.Executor = newExecutor;

            return new ServiceResult<TaskDbEntity>(task);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while updating task executor");
        }
    }

    public ServiceResult<TaskDbEntity> SetInspector(Guid? inspector, TaskDbEntity task)
    {
        try
        {
            if (inspector != task.Inspector)
                task.Inspector = inspector;

            return new ServiceResult<TaskDbEntity>(task);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while updating task executor");
        }
    }

    public ServiceResult<TaskDbEntity> UpdateTask(TaskDbEntity taskDbEntity, string name, string? description)
    {
        try
        {
            taskDbEntity.Name = name;
            taskDbEntity.Description = description;
            return new ServiceResult<TaskDbEntity>(taskDbEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while updating task");
        }
    }

    public ServiceResult<TaskDbEntity> CreateTask(string name, string? description, Guid author,
        TaskStatusDbEntity status,
        Guid? executor, Guid? inspector)
    {
        try
        {
            var task = new TaskDbEntity
            {
                Name = name,
                Description = description,
                Created = DateTime.Now,
                Author = author,
                Status = status,
                Executor = executor,
                Inspector = inspector,
            };

            return new ServiceResult<TaskDbEntity>(task);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while creating task");
        }
    }
}