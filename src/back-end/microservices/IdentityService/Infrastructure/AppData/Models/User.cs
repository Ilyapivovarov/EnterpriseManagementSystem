using IdentityService.Infrastructure.AppData.Models.Base;

namespace IdentityService.Infrastructure.AppData.Models;

public class User : DbEntityBase
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirsName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}