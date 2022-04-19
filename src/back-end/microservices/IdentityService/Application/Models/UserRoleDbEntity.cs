using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace IdentityService.Application.Models;

public class UserRoleDbEntity : DbEntityBase
{
    public string Name { get; set; }
}