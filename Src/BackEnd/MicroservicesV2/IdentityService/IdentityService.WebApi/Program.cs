using EnterpriseManagementSystem.Logging;
using IdentityService.WebApi;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
        webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

await host.RunAsync();