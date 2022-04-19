using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace IdentityService.Application.Models;

[Index(nameof(Email), IsUnique = true)]
public class EmailAddressDbEntity : DbEntityBase
{
    [EmailAddress] 
    public string Email { get; set; }

    public bool IsVerified { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
}