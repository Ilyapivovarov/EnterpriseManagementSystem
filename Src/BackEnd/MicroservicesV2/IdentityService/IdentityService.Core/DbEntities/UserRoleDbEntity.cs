using IdentityService.Core.DbEntities.Base;

namespace IdentityService.Core.DbEntities;

public class UserRoleDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;
}