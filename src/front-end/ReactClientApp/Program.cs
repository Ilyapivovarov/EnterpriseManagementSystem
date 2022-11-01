using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ReactClientApp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

await host.RunAsync();
