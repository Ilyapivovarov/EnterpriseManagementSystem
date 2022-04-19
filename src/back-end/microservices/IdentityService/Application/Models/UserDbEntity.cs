using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace IdentityService.Application.Models;

public class UserDbEntity : DbEntityBase
{
    public string Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    [ForeignKey("EmailAddressesId")]
    public virtual EmailAddressDbEntity EmailAddress { get; set; }

    [ForeignKey("UserRoleId")]
    public virtual UserRoleDbEntity Role { get; set; }
}