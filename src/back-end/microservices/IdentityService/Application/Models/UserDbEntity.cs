#pragma warning disable CS8618

namespace IdentityService.Application.Models;

public class UserDbEntity : DbEntityBase
{
    public string Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public EmailAddressDbEntity EmailAddress { get; set; }

    public UserRole Role { get; set; } = UserRole.Reader;
}