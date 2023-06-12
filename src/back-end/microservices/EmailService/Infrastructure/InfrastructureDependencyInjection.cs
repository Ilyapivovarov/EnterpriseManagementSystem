using EnterpriseManagementSystem.MessageBroker;

namespace EmailService.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastucture(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        #region SmtpClient

        services.AddScoped(_ => new SmtpClient
        {
            Host = configuration.GetValue<string>("Smtp:Host"),
            Port = configuration.GetValue<int>("Smtp:Port"),
            Credentials = new NetworkCredential(
                configuration.GetValue<string>("Smtp:Username"),
                configuration.GetValue<string>("Smtp:Password")
            ),
            EnableSsl = true
        });

        #endregion

        #region Register MassTransisist

        services.AddMessageBroker(configuration.GetConnectionString("RabbitMq"));

        #endregion

        #region Register services

        services.AddTransient<IEmailService, EmailSerivce>();

        #endregion
    }
}