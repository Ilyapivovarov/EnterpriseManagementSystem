namespace TaskService.Core.DbEntities;

public class UserDbEntity : DbEntityBase
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public Guid IdentityGuid { get; set; }
}