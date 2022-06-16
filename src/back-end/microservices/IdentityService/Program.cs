var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();


var context = host.Services.GetRequiredService<ApplicationDbContext>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();
await ApplicationDbContextSeed.SeedData(context, logger);

await host.RunAsync();