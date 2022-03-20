using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var dbConnectionSectionName = environment.IsDevelopment() ? "DevelopDbConnection" : "ReleaseDbConnection";
        serviceProvider.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(configuration.GetConnectionString(dbConnectionSectionName)));
    }
}