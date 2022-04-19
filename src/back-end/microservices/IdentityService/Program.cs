using IdentityService;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextSeed>>();
await ApplicationDbContextSeed.SeedData(context, logger);

await host.RunAsync();