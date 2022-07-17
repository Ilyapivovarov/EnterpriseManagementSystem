using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Core.DbEntities.Base;

[Index(nameof(Guid), IsUnique = true)]
public abstract class DbEntityBase
{
    [Key]
    public int Id { get; protected set; }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Guid { get; protected set; } = Guid.NewGuid();

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public bool IsDeleted { get; set; }
}