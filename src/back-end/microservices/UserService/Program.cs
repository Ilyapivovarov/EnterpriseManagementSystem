using Microsoft.AspNetCore;
using UserService;

var host = WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build();

await host.RunAsync();
    