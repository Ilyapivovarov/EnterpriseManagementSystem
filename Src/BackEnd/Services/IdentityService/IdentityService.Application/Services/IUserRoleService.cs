using IdentityService.Core.DbEntities;

namespace IdentityService.Application.Services;

public interface IUserRoleService
{
    public Task<UserRoleDbEntity> GetOrCreateAndReturn(string name);
}