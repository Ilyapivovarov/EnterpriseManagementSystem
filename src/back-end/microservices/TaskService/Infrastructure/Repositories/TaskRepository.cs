using TaskService.Infrastructure.Repositories.Base;

namespace TaskService.Infrastructure.Repositories;

public sealed class TaskRepository : RepositoryBase, ITaskRepository
{
    public TaskRepository(ITaskDbContext taskDbContext, ILogger<TaskRepository> logger)
        : base(taskDbContext, logger)
    { }

    public async Task<int> GetTasksCount()
    {
        return await LoadDataAsync(db => db.Tasks.Count());
    }

    public async Task<TaskDbEntity?> GetTaskByIdAsync(int id)
    {
        return await LoadDataAsync(db => db.Tasks.FirstOrDefault(x => x.Id == id));
    }

    public async Task<TaskDbEntity?> GetTaskByGuidAsync(Guid guid)
    {
        return await LoadDataAsync(db => db.Tasks.FirstOrDefault(x => x.Guid == guid));
    }

    public async Task<TaskDbEntity[]?> GetTasksByPage(int pageNumber, int pageSize)
    {
        return await LoadDataAsync(db => db.Tasks.Skip(pageSize * pageNumber - pageSize)
            .Take(pageSize)
            .ToArray());
    }

    public async Task<bool> UpdateAsync(TaskDbEntity taskDbEntity)
    {
        return await WriteDataAsync(x => x.Tasks.Update(taskDbEntity));
    }

    public async Task<bool> SaveTaskAsync(TaskDbEntity taskDbEntity)
    {
        return await WriteDataAsync(db => db.Tasks.Add(taskDbEntity));
    }
}
