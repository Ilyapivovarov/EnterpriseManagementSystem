namespace IdentityService.Infrastructure.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<UserRoleDbEntity> GetOrCreateAndReturn(string name)
    {
        var existedRole = await _userRoleRepository.GetByName(name);
        if (existedRole != null)
            return existedRole;

        var userRole = new UserRoleDbEntity
        {
            Name = name
        };

        await _userRoleRepository.SaveRange(userRole);
        
        return userRole;
    }
}