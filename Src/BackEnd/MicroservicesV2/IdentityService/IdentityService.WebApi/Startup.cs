using EnterpriseManagementSystem.JwtAuthorization;
using EnterpriseManagementSystem.JwtAuthorization.Middlewares;
using IdentityService.Application;
using IdentityService.Infrastructure;

namespace IdentityService.WebApi;

public sealed class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    private IConfiguration Configuration { get; }

    private IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication(Configuration, Environment);
        services.AddInfrastructure(Configuration, Environment);
    
        services.AddRouting(options =>
            options.LowercaseUrls = true);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        
        app.UseAuthentication();  
        app.UseAuthorization();

        app.UseMiddleware<JwtSessionMiddleware>();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers()
            .RequireAuthorization());
    }
}