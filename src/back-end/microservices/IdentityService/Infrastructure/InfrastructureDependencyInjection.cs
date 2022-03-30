using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register context

        var dbConnectionSectionName = environment.IsDevelopment() ? "DevelopDbConnection" : "ReleaseDbConnection";
        serviceProvider.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(configuration.GetConnectionString(dbConnectionSectionName)));

        #endregion

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

        #region Register repositories

        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();
        serviceProvider.AddTransient<IUserRepository, UserRepository>();
        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();

        #endregion
        
        #region Register services

        serviceProvider.AddTransient<IAuthService, AuthService>();
        serviceProvider.AddTransient<ISecurityService, SecurityService>();

        #endregion
    }
}