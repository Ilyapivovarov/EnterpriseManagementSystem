using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Core.DbEntities;

[Index(nameof(Address), IsUnique = true)]
public class EmailAddressDbEntity : DbEntityBase
{
    [EmailAddress]
    public string Address { get; set; } = null!;

    public bool IsVerified { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
}