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
    }
}