using System.ComponentModel.DataAnnotations;

namespace UserService.Core.DbEntities.Base;

[Index(nameof(Guid), IsUnique = true)]
public abstract class DbEntityBase
{
    [Key]
    public int Id { get; protected set; }

    public Guid Guid { get; protected set; } = Guid.NewGuid();

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool IsDeleted { get; set; }
}