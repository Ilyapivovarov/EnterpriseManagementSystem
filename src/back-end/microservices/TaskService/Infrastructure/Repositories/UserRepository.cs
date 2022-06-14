using TaskService.Infrastructure.Repositories.Base;

namespace TaskService.Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ITaskDbContext taskDbContext, ILogger<UserRepository> logger)
        : base(taskDbContext, logger)
    { }

    public async Task<bool> SaveUserDbEntityAsync(UserDbEntity userDbEntity)
    {
        return await WriteDataAsync(x => x.Users.Add(userDbEntity));
    }

    public async Task<UserDbEntity?> GetUserByGuid(Guid guid)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.Guid == guid));
    }
}