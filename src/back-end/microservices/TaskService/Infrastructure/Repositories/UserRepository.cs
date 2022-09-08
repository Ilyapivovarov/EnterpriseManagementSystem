using TaskService.Infrastructure.Repositories.Base;

namespace TaskService.Infrastructure.Repositories;

public sealed class UserRepository : SqlRepositoryBase, IUserRepository
{
    public UserRepository(ITaskDbContext taskDbContext, ILogger<UserRepository> logger)
        : base(taskDbContext, logger)
    { }

    public async Task<bool> SaveAsync(UserDbEntity userDbEntity)
    {
        return await WriteDataAsync(x => x.Users.Add(userDbEntity));
    }

    public async Task<UserDbEntity?> GetUserByGuid(Guid guid)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.Guid == guid));
    }

    public async Task<UserDbEntity?> GetUserByIdentityGuid(Guid guid)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.IdentityGuid == guid));
    }

    public async Task<UserDbEntity?> GetUserById(int? id)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.Id == id));
    }

    public async Task<UserDbEntity[]?> GetUsersByPage(int start, int count)
    {
        return await LoadDataAsync(db => db.Users.OrderBy(x => x.EmailAddress)
            .Skip(start)
            .Take(count)
            .ToArray());
    }

    public async Task<int> Count()
    {
        return await LoadDataAsync(db => db.Users.Count());
    }
}
