var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

// await TaskDbContextSeed.InitData(host.Services);

await host.RunAsync();