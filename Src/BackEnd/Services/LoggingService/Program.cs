using LoggingService;
using LoggingService.MongoDbStorage;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMongoDbStorage();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();