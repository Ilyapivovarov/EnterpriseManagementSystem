var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build();


var mediator = host.Services.GetRequiredService<IMediator>();
await mediator.Send(AuthRequest<SignUp>.Create(new SignUp("Admin", "Admin", "admin@admin.com", "admin", "admin")));

await host.RunAsync();