using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

public class EployeeDbEntity : DbEntityBase
{
    public UserDbEntity User { get; set; } = null!;

    public PositionDbEntity? Position { get; set; }
}