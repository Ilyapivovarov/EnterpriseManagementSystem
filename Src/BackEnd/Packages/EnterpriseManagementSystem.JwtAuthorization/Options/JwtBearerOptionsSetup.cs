using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace EnterpriseManagementSystem.JwtAuthorization.Options;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            
            ValidateAudience = false,

            ValidateLifetime = true,

            IssuerSigningKey = _options.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
         
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}