using LogWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        
    })
    .Build();

host.Run();