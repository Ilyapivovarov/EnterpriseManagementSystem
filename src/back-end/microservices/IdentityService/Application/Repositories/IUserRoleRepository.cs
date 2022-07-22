namespace IdentityService.Application.Repositories;

public interface IUserRoleRepository
{
    Task<UserRoleDbEntity?> GetReaderRole();

    Task<UserRoleDbEntity?> GetAdminRole();

    Task<bool> SaveRange(params UserRoleDbEntity[] userRoleDbEntities);
}