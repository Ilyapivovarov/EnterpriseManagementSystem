using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register context

        var conString = configuration.GetConnectionString("DefaultConnection");
        serviceProvider.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(conString));

        #endregion
        
        #region Register repositories

        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();
        serviceProvider.AddTransient<IUserRepository, UserRepository>();
        serviceProvider.AddTransient<ISessionRepository, SessionRepository>();

        #endregion
        
        #region Register services
        
        serviceProvider.AddTransient<ISecurityService, SecurityService>();

        #endregion

        #region Bl services

        serviceProvider.AddTransient<IUserBlService, UserBlService>();
        serviceProvider.AddTransient<ISessionBlService, SessionBlService>();

        #endregion

        #region Mediators

        serviceProvider.AddTransient<ISignInMediator, SignInMediator>();
        serviceProvider.AddTransient<ISignUpMediator, SignUpMediator>();
        serviceProvider.AddTransient<ISignOutMediator, SignOutMediator>();

        #endregion
    }
}