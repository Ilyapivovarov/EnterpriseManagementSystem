using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization;

public class AuthOption
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int TokenLifetime { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}