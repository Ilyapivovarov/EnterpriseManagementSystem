using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Core.DbEntities.Base;

[Index(nameof(Guid), IsUnique = true)]
public abstract class DbEntityBase
{
    [Key]
    public int Id { get; protected set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; protected set; } = Guid.NewGuid();
}