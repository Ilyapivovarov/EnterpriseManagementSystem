using LogWorkerService.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var host = builder.Build();

var logWorkerDbContext = host.Services.CreateScope().ServiceProvider.GetRequiredService<ILogWorkerDbContext>();
await logWorkerDbContext.MigrateAsync();
await host.RunAsync();
