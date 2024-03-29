using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Core.DbEntities.Base;

public abstract class DbEntityBase
{
    public int Id { get; protected set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; protected set; } = Guid.NewGuid();
}