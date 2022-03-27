using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Common;

public class AuthOption
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Secret { get; set; }

    public int TokenLifetime { get; set; }

    public  SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}