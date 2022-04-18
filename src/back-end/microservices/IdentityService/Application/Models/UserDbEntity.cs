using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Models;

[Index(nameof(Email), IsUnique = true)]
public class UserDbEntity : DbEntityBase
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public UserRole Role { get; set; } = UserRole.Reader;
}