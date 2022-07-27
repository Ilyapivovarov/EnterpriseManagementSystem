using System.ComponentModel.DataAnnotations;

namespace TaskService.Core.DbEntities.Base;

public abstract class DbEntityBase
{
    [Key]
    public int Id { get; protected set; }

    public Guid Guid { get; protected set; } = Guid.NewGuid();
}
