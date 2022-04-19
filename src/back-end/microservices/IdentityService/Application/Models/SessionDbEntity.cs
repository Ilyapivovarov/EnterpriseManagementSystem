using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Models;

[Index(nameof(AccessToken), IsUnique = true)]
[Index(nameof(RefreshToken), IsUnique = true)]
public class SessionDbEntity : DbEntityBase
{
    public string AccessToken { get; set; } = null!;

    public Guid RefreshToken { get; set; } = Guid.NewGuid();

    [ForeignKey("UserId")] public virtual UserDbEntity User { get; set; } = null!;
}