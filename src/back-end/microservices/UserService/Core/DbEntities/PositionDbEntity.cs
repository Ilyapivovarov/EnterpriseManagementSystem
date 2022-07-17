using System.ComponentModel.DataAnnotations.Schema;
using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

[Table("Positions")]
public class PositionDbEntity : DbEntityBase
{
    public string Name { get; set; } = null!;
}