using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization;

public sealed class AuthOption
{
    public string Issuer => "https://localhost:7104;http://localhost:5104";

    public int TokenLifetime => 900000;

    private string Secret => "secretKey1234567890-";

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
