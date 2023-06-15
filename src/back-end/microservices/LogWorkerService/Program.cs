using LogWorkerService.Infrastructure;
using LogWorkerService.Infrastructure.DbContexts;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var host = builder.Build();

var logWorkerDbContext = host.Services.GetRequiredService<LogWorkerDbContext>();
await logWorkerDbContext.Database.MigrateAsync();
await host.RunAsync();