using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

public class EmployeeDbEntity : DbEntityBase
{
    public int UserDbEntityId { get; set; }
    
    public virtual UserDbEntity User { get; set; } = null!;

    public virtual PositionDbEntity? Position { get; set; }
}