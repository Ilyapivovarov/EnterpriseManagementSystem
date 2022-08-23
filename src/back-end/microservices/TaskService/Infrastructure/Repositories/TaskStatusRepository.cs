using TaskService.Infrastructure.Repositories.Base;

namespace TaskService.Infrastructure.Repositories;

public sealed class TaskStatusRepository : RepositoryBase, ITaskStatusRepository
{
    public TaskStatusRepository(ITaskDbContext taskDbContext, ILogger<TaskStatusRepository> logger)
        : base(taskDbContext, logger)
    { }

    public async Task<bool> SaveAsync(TaskStatusDbEntity taskStatusDbEntity)
    {
        return await WriteDataAsync(db => db.TaskStatuses.Add(taskStatusDbEntity));
    }

    public async Task<TaskStatusDbEntity?> GetById(int? id)
    {
        return await LoadDataAsync(db => db.TaskStatuses
            .FirstOrDefault(x => x.Id == id));
    }

    public async Task<TaskStatusDbEntity?> GetByGuid(Guid guid)
    {
        return await LoadDataAsync(db => db.TaskStatuses.FirstOrDefault(x => x.Guid == guid));
    }

    public async Task<TaskStatusDbEntity> GetDefaultTaskStatus()
    {
        var defaultTaskStatus = await LoadDataAsync(db => db.TaskStatuses.First());

        if (defaultTaskStatus == null)
            throw new Exception("Not found default status");

        return defaultTaskStatus;
    }

    public async Task<TaskStatusDbEntity?> GetByName(string statusName)
    {
        return await LoadDataAsync(db => db.TaskStatuses.FirstOrDefault(x => x.Name == statusName));
    }

    public async Task<TaskStatusDbEntity[]?> GetAllStatuses()
    {
        return await LoadDataAsync(db => db.TaskStatuses.ToArray());
    }
}
