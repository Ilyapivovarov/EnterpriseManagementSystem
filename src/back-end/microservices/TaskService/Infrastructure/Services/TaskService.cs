namespace TaskService.Infrastructure.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskStatusRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TaskService(IUserRepository userRepository, ITaskStatusRepository taskStatusRepository)
    {
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

    public bool UpdateTaskStatus(TaskStatusDbEntity taskStatusDbEntity, TaskDbEntity taskDbEntity)
    {
        throw new NotImplementedException();
    }
}
