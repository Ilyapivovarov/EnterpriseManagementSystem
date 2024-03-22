using EnterpriseManagementSystem.Helpers.Extensions;
using LoggingService;
using LoggingService.AppContext;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<LoggingServiceContext>(options =>
{
    var dbConnectionString = builder.Configuration.GetRequiredConnectionString("Mongo");
    options.UseMongoDB(dbConnectionString, $"ems_log_{builder.Environment.EnvironmentName}");
    // options.UseLazyLoadingProxies();
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();