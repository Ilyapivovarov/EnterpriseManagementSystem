using EnterpriseManagementSystem.MessageBroker;

namespace TaskService.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        #region Register TaskDbContext

        services.AddDbContext<TaskDbContext>(builder =>
        {
            builder = environment.IsEnvironment("Testing")
                ? builder.UseInMemoryDatabase(configuration.GetConnectionString("SqlServer"))
                : builder.UseSqlServer(configuration.GetConnectionString("SqlServer"));

            builder.UseLazyLoadingProxies();
        });


        services.AddScoped<ITaskDbContext, TaskDbContext>();

        #endregion

        #region Registre repositories

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<ITaskStatusRepository, TaskStatusRepository>();

        #endregion

        #region Register JWT

        services.AddJwtAuthorization();

        #endregion

        #region Register services

        services.AddTransient<ITaskService, Services.TaskService>();

        #endregion

        #region Register HostedServices

        services.AddHostedService<DefaultDataSeedServices>();

        #endregion

        #region Register MassTransisist

        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq"));

        #endregion
    }
}
