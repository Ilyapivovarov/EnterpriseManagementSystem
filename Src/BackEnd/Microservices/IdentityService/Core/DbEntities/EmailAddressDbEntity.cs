using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Core.DbEntities;

[Index(nameof(Address), IsUnique = true)]
public class EmailDbEntity : DbEntityBase
{
    [EmailAddress]
    public EmailAddress Address { get; set; }

    public bool IsVerified { get; set; }
}