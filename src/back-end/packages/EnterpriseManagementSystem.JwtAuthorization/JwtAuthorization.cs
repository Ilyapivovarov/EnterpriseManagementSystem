using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization;

public static class JwtAuthorization
{
    public static void AddJwtAuthorization(this IServiceCollection serviceProvider, IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtBearer");
        serviceProvider.Configure<AuthOption>(section);
        var authOpt = section.Get<AuthOption>();
        serviceProvider.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOpt.Issuer,
                    
                    ValidateAudience = false,
                    
                    ValidateLifetime = true,

                    IssuerSigningKey = authOpt.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
    }
}