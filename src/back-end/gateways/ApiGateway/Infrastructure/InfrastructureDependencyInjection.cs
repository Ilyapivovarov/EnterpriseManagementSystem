using ApiGateway.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceProvider.AddHttpClient<IAuthHttpClientService, AuthHttpClientService>(client =>
        {
            client.BaseAddress = new Uri(configuration["IdentityServiceUrl"]);
        });
        
        #region Register Jwt auth

        const string AuthSectionKey = "Auth";
        
        var section = configuration.GetSection(AuthSectionKey);
        serviceProvider.Configure<AuthOption>(section);
        
        var authOpt = configuration.GetSection(AuthSectionKey).Get<AuthOption>();
        serviceProvider.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = authOpt.Issuer,

                    ValidateAudience = false,
                    ValidAudience = authOpt.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOpt.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });


        #endregion
    }
}