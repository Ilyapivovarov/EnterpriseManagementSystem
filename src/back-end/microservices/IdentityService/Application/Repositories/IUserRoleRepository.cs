namespace IdentityService.Application.Repositories;

public interface IUserRoleRepository
{
    Task<UserRoleDbEntity?> GetByName(string name);

    Task<UserRoleDbEntity?> GetReaderRole();

    Task<UserRoleDbEntity?> GetAdminRole();

    Task<bool> Save(UserRoleDbEntity userRoleDbEntitie);
    
    Task<bool> SaveRange(params UserRoleDbEntity[] userRoleDbEntities);
}