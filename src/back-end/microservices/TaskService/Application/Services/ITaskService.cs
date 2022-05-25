namespace TaskService.Application.Services;

public interface ITaskService
{
    public Task<UsersInvolvedInTask> GetUsersInvolvedInTask(Guid authorGuid, Guid? executorGuid = null, Guid? inspectorGuid = null, Guid[]? observerGuids = null);
}