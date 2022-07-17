using System.ComponentModel.DataAnnotations.Schema;
using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

[Table("Employees")]
public class EmployeeDbEntity : DbEntityBase
{
    public virtual UserDbEntity UserDbEntity { get; set; } = null!;
    public virtual PositionDbEntity? Position { get; set; }
}