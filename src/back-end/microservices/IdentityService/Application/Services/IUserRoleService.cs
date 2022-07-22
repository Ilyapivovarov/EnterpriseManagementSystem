namespace IdentityService.Application.Services;

public interface IUserRoleService
{
    public Task<ServiceActionResult<UserRoleDbEntity>> GetOrCreateAndReturn(string name);
}