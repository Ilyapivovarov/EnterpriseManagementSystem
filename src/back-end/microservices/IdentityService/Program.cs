using Microsoft.AspNetCore;

var host = WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build();

await host.RunAsync();