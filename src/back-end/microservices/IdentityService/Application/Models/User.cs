namespace IdentityService.Application.Models;

public class User : DbEntityBase
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public Guid Guid { get; set; } = Guid.NewGuid();
}