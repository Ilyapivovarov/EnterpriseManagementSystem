using TaskService.Infrastructure.Repositories.Base;

namespace TaskService.Infrastructure.Repositories;

public sealed class TaskStatusRepository : RepositoryBase, ITaskStatusRepository
{
    public TaskStatusRepository(ITaskDbContext taskDbContext, ILogger<TaskStatusRepository> logger)
        : base(taskDbContext, logger)
    { }

    public async Task<TaskStatusDbEntity?> GetByGuid(Guid guid)
    {
        return await LoadDataAsync(db => db.TaskStatuses.FirstOrDefault(x => x.Guid == guid));
    }

    public async Task<TaskStatusDbEntity?> GetByName(string statusName)
    {
        return await LoadDataAsync(db => db.TaskStatuses.FirstOrDefault(x => x.Name == statusName));
    }
}