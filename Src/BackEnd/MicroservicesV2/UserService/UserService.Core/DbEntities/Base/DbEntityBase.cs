namespace UserService.Core.DbEntities.Base;

public abstract class DbEntityBase
{
    [Key]
    public int Id { get; protected set; }
    
    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool IsDeleted { get; set; }
}