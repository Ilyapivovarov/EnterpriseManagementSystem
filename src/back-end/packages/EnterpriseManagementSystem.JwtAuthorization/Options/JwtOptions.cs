using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization.Options;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = null!;
    
    public string SecretKey { get; set; } = null!;
    
    public int TokenLifetime { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8 .GetBytes(SecretKey));
    }
}