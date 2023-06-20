using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization.Models;

public class JwtToken
{
    #region Fields

    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly JwtSecurityToken _jwtSecurityToken;

    #endregion

    public JwtToken(string issuer, DateTime expiry, IEnumerable<Claim> claims, SigningCredentials credentials)
    {
        Issuer = issuer;
        Expiry = expiry;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _jwtSecurityToken = new JwtSecurityToken(
            issuer,
            claims: claims,
            expires: expiry,
            signingCredentials: credentials);
    }

    public string Issuer { get; }

    public DateTime Expiry { get; }

    public TimeSpan GetExpirationTime() => Expiry - DateTime.Now;

    public override string ToString()
    {
        return _jwtSecurityTokenHandler.WriteToken(_jwtSecurityToken);
    }
}