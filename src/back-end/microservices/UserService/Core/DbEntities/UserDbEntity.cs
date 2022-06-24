using UserService.Core.DbEntities.Base;

namespace UserService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateBrith { get; set; }
}