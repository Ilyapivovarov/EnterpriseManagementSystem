using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Application.DbModels.Base;

[Index(nameof(Guid), IsUnique = true)]
public class DbModelBase
{
    [Key]
    public int Id { get;  protected set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; protected set; } = Guid.NewGuid();
}