using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        #region Register DbContext

        var dbConnectionSectionName = environment.IsDevelopment() ? "DevelopDbConnection" : "ReleaseDbConnection";
        serviceProvider.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(configuration.GetConnectionString(dbConnectionSectionName)));

        #endregion
    }
}