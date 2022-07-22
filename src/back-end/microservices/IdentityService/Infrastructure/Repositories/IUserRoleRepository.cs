using UserService.Core;

namespace IdentityService.Infrastructure.Repositories;

public class UserRoleRepository : RepositoryBase, IUserRoleRepository
{
    public UserRoleRepository(IIdentityDbContext dbContext, ILogger<UserRoleRepository> logger)
        : base(dbContext, logger)
    { }

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

    public async Task<bool> SaveRange(params UserRoleDbEntity[] userRoleDbEntities)
    {
        return await WriteDataAsync(db => db.UserRoles.AddRange(userRoleDbEntities));
    }
}