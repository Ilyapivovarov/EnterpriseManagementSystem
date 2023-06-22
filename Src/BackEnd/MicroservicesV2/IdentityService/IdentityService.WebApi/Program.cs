var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
        webBuilder => webBuilder.UseStartup<Startup>())
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    await db.Database.MigrateAsync();
}

await host.RunAsync();