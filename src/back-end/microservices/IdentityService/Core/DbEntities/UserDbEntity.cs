using System.ComponentModel.DataAnnotations.Schema;
using IdentityService.Core.DbEntities.Base;
// ReSharper disable All

namespace IdentityService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [ForeignKey("EmailAddressesId")] 
    public virtual EmailAddressDbEntity EmailAddress { get; set; } = null!;

    [ForeignKey("UserRoleId")] 
    public virtual UserRoleDbEntity Role { get; set; }  = null!;
}