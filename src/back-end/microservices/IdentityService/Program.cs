using IdentityService;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();
    
// using (var scope = host.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     ApplicationDbContextSeed.SeedData(services);
// }

await host.RunAsync();