using System.ComponentModel.DataAnnotations.Schema;
using IdentityService.Application.Models.Base;

namespace IdentityService.Application.Models;

public class Session : DbEntityBase
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}