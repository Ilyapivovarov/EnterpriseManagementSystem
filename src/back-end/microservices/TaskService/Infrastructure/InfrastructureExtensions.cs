using EnterpriseManagementSystem.JwtAuthorization;
using TaskService.Infrastructure.Repositories;

namespace TaskService.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        #region Register TaskDbContext

        services.AddDbContext<TaskDbContext>(builder => builder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<ITaskDbContext, TaskDbContext>();

        #endregion

        #region Registre repositories

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<ITaskStatusRepository, TaskStatusRepository>();

        #endregion

        #region Register JWT

        services.AddJwtAuthorization(configuration);

        #endregion

        #region Register services

        services.AddTransient<ITaskService, Services.TaskService>();

        #endregion
    }
}