namespace IdentityService.Infrastructure.Repositories;

public class UserRoleRepository : SqlRepositoryBase, IUserRoleRepository
{
    public UserRoleRepository(IIdentityDbContext dbContext, ILogger<UserRoleRepository> logger)
        : base(dbContext, logger)
    { }

    public async Task<UserRoleDbEntity?> GetByName(string name)
    {
        return await LoadDataAsync(db => db.UserRoles.FirstOrDefault(x => x.Name == name));
    }

    public async Task<UserRoleDbEntity?> GetReaderRole()
    {
        return await LoadDataAsync(db => db.UserRoles.FirstOrDefault(x
            => x.Name == DefaultUserRoleNames.Reader));
    }

    public async Task<UserRoleDbEntity?> GetAdminRole()
    {
        return await LoadDataAsync(db => db.UserRoles.FirstOrDefault(x
            => x.Name == DefaultUserRoleNames.Admin));
    }

    public async Task<bool> Save(UserRoleDbEntity userRoleDbEntitie)
    {
        return await WriteDataAsync(db => db.UserRoles.Add(userRoleDbEntitie));
    }

    public async Task<bool> SaveRange(params UserRoleDbEntity[] userRoleDbEntities)
    {
        return await WriteDataAsync(db => db.UserRoles.AddRange(userRoleDbEntities));
    }
}