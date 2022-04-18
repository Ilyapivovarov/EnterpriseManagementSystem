using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Models;

public class SessionDbEntity : DbEntityBase
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    [ForeignKey("UserId")] 
    public UserDbEntity User { get; set; } = null!;
}