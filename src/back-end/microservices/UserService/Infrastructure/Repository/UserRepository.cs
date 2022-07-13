using UserService.Infrastructure.Repository.Base;

namespace UserService.Infrastructure.Repository;

public sealed class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ILogger<UserRepository> logger, IUserDbContext userDbContext)
        : base(logger, userDbContext)
    { }

    public async Task<UserDbEntity?> GetByIdAsync(int id)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.Id == id));
    }

    public async Task<UserDbEntity?> GetByGuidAsync(Guid guid)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.IdentityGuid == guid));
    }

    public async Task<UserDbEntity?> GetByEmailAddressAsync(string emailAddress)
    {
        return await LoadDataAsync(db => db.Users.FirstOrDefault(x => x.EmailAddress == emailAddress));
    }

    public async Task<ICollection<UserDbEntity>?> GetUsersByRange(int rangeStart, int rangeEnd)
    {
        return await LoadDataAsync(db => db.Users.OrderBy(x => x.EmailAddress)
            .Skip(rangeStart)
            .Take(rangeEnd)
            .ToArray());
    }

    public async Task<bool> SaveAsync(UserDbEntity userDbEntity)
    {
        return await WriteDataAsync(db => db.Users.Add(userDbEntity));
    }

    public async Task<bool> UpdateAsync(UserDbEntity userDbEntity)
    {
        return await WriteDataAsync(db => db.Users.Update(userDbEntity));
    }

    public async Task<bool> RemoveAsync(UserDbEntity userDbEntity)
    {
        return await WriteDataAsync(db => db.Users.Remove(userDbEntity));
    }
}