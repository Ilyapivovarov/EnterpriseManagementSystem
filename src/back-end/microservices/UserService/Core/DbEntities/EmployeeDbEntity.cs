namespace UserService.Core.DbEntities;

public class EmployeeDbEntity : DbEntityBase
{
    public virtual UserDbEntity UserDbEntity { get; set; } = null!;
    public virtual PositionDbEntity? Position { get; set; }
}