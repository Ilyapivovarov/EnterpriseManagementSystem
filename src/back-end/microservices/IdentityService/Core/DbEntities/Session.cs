using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Core.DbEntities;

public class Session
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public UserDbEntity User { get; set; } = null!;
}