using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Models.Base;

[Index(nameof(Guid), IsUnique = true)]
public abstract class DbEntityBase
{
    public int Id { get; protected set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; protected set; } = Guid.NewGuid();
}