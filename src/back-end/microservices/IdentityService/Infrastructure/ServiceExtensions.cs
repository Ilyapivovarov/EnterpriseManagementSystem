
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection serviceProvider, IConfiguration configuration)
    {
        serviceProvider.AddDbContext<ApplicationDbContext>(builder => 
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}