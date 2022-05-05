namespace EmailService;

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
        services.AddAppication(Configuration, Environment);
        services.AddInfrastucture(Configuration, Environment);
        
        services.AddRouting(options =>
            options.LowercaseUrls = true);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment()) 
            app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers()
                .RequireAuthorization();
        });
    }
}