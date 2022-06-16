var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

await IdentityDbContextSeed.InitData(host.Services);

await host.RunAsync();