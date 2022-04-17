using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddMediatR(Assembly.GetExecutingAssembly());
        serviceProvider.AddAutoMapper(Assembly.GetExecutingAssembly());

        #region Register Jwt auth

        var section = configuration.GetSection(ConfigurationSectionName.Auth);
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

                ValidateAudience = true,
                ValidAudience = authOpt.Audience,

                ValidateLifetime = true,

                IssuerSigningKey = authOpt.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });

        #endregion
    }
}