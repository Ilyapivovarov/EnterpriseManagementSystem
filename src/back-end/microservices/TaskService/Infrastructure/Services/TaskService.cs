namespace TaskService.Infrastructure.Services;

public sealed class TaskService : ITaskService
{
    private readonly IUserRepository _userRepository;

    public TaskService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
}