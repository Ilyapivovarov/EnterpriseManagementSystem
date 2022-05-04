#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Core.DbEntities;

[Index(nameof(UserGuid), IsUnique = true)]
public class AuthorDbEntity : DbEntityBase
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [ForeignKey("EmailAddressesId")]
    public virtual EmailAddressDbEntity EmailAddress { get; set; }

    [Required]
    public Guid UserGuid { get; set; }
}