using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register context

        var conString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(builder =>
            builder
                .UseLazyLoadingProxies()
                .UseSqlServer(conString));

        #endregion

        #region Register repositories

        services.AddTransient<ISessionRepository, SessionRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISessionRepository, SessionRepository>();

        #endregion

        #region Register services

        services.AddTransient<ISecurityService, SecurityService>();

        #endregion

        #region Register bl services

        services.AddTransient<IUserBlService, UserBlService>();
        services.AddTransient<ISessionBlService, SessionBlService>();

        #endregion

        #region Register Jwt auth

        services.AddJwtAuthorization(configuration);

        #endregion

        #region Register event bus

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        #endregion

        services.AddCors();
    }
}