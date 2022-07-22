namespace IdentityService.Infrastructure.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<ServiceActionResult<UserRoleDbEntity>> GetOrCreateAndReturn(string name)
    {
        var existedRole = await _userRoleRepository.GetByName(name);
        if (existedRole != null)
            return new ServiceActionResult<UserRoleDbEntity>(existedRole);

        var userRole = new UserRoleDbEntity
        {
            Name = name
        };

        var saveResult = await _userRoleRepository.SaveRange(userRole);
        return saveResult
            ? new ServiceActionResult<UserRoleDbEntity>(userRole)
            : new ServiceActionResult<UserRoleDbEntity>("Error while saving user role");

    }
}