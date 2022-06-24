using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

public class PositionDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;
}