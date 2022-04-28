#pragma warning disable CS8618

namespace IdentityService.Core.DbEntities;

public class UserRoleDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;
}