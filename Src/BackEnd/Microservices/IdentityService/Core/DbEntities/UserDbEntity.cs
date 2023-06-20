using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public Password Password { get; set; }

    [ForeignKey("EmailId")]
    public virtual EmailDbEntity Email { get; set; } = null!;

    [ForeignKey("UserRoleId")]
    public virtual UserRoleDbEntity Role { get; set; } = null!;
}