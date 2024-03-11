namespace EnterpriseManagementSystem.JwtAuthorization.Models;

public class JwtTokenEncode
{
    public string Email { get; set; } = null!;

    public Guid Sub { get; set; }

    public string Role { get; set; } = null!;

    public DateTime Exp { get; set; }

    public string Iss { get; set; } = null!;
}