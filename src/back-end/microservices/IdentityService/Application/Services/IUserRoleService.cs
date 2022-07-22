namespace IdentityService.Application.Services;

public interface IUserRoleService
{
    public Task<UserRoleDbEntity> GetOrCreate(string name);
}