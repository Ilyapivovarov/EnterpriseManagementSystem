namespace UserService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public EmailAddress EmailAddress { get; set; }

    public DateTime? DateBrith { get; set; }

    public Guid IdentityGuid { get; set; }
}