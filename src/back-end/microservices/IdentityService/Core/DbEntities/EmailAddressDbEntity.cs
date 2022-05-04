using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Core.DbEntities;

[Index(nameof(Email), IsUnique = true)]
public class EmailAddressDbEntity : DbEntityBase
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    public bool IsVerified { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
}