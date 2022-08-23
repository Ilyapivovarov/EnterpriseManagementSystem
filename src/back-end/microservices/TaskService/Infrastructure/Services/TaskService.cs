namespace TaskService.Infrastructure.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskStatusRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;
    private readonly IUserRepository _userRepository;

    public TaskService(ILogger<TaskService> logger, IUserRepository userRepository,
        ITaskStatusRepository taskStatusRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _taskRepository = taskStatusRepository;
    }

    public async Task<UsersInvolvedInTask> GetUsersInvolvedInTask(Guid authorGuid, Guid? executorGuid = null,
        Guid? inspectorGuid = null,
        ICollection<Guid>? observerGuids = null)
    {
        var user = await _userRepository.GetUserByGuid(authorGuid);

        // TODO: Доделать

        if (user == null)
            throw new Exception("Not found user with guid");

        return new UsersInvolvedInTask(user);
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

    public ServiceResult<TaskDbEntity> SetExecutor(TaskDbEntity task, UserDbEntity? newExecutor)
    {
        try
        {
            if (task.Executor?.Id != newExecutor?.Id)
                task.Executor = newExecutor;
            
            return new ServiceResult<TaskDbEntity>(task);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ServiceResult<TaskDbEntity>("Error while updating task executor");
        }
    }

    public ServiceResult<TaskDbEntity> SetInspector(UserDbEntity? inspector, TaskDbEntity task)
    {
        try
        {
            if (inspector?.Id != task.Inspector?.Id)
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

    public ServiceResult<TaskDbEntity> CreateTask(string name, string? description, UserDbEntity author, TaskStatusDbEntity status,
        UserDbEntity? executor, UserDbEntity? inspector)
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
