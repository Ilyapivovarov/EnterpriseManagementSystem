using ApiGateway.Application.HttpClients;
using ApiGateway.Infrastructure.Handlers;
using ApiGateway.Infrastructure.HttpClients;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiGateway.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        //register delegating handlers
        serviceProvider.AddHttpContextAccessor();
        serviceProvider.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        
        serviceProvider.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["IdentityServiceUrl"]);
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        #region Register Jwt auth

        var section = configuration.GetSection(ConfigurationSectionName.Auth);
        serviceProvider.Configure<AuthOption>(section);

        var authOpt = configuration.GetSection(ConfigurationSectionName.Auth).Get<AuthOption>();
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