﻿using EnterpriseManagementSystem.JwtAuthorization.Infrasturcture;
using EnterpriseManagementSystem.JwtAuthorization.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseManagementSystem.JwtAuthorization;

public static class JwtAuthorization
{
    public static void AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtBearer");
        services.Configure<AuthOption>(section);
        var authOpt = section.Get<AuthOption>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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


        services.AddTransient<IJwtSessionService, JwtSessionService>();
    }
}