using System.ComponentModel.DataAnnotations.Schema;
using EnterpriseManagementSystem.BusinessModels;
using IdentityService.Core.DbEntities.Base;

namespace IdentityService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public Password Password { get; set; }

    [ForeignKey("EmailId")]
    public virtual EmailDbEntity Email { get; set; } = null!;

    [ForeignKey("UserRoleId")]
    public virtual UserRoleDbEntity Role { get; set; } = null!;
}