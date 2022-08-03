using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TaskService.Application.DbContexts;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.DbSeedTests;

public sealed class TaskDbContextSeedTests : TestBase
{
    [Test]
    public async Task SeedDefatulDatatTest()
    {
        using var services = Server.Services.CreateScope();

        var @event = new SignUpUserIntegrationEvent(new UserDataResponse(Guid.NewGuid(), "Admin", "Admin",
            "admin@admin.com", DateTime.Now));

        var bus = services.ServiceProvider.GetRequiredService<IBus>();
        var endPoint = await bus.GetPublishSendEndpoint<SignUpUserIntegrationEvent>();
        await endPoint.Send(@event);

        Thread.Sleep(10000);

        Assert.NotZero(services.ServiceProvider.GetRequiredService<ITaskDbContext>().TaskStatuses.Count());
        Assert.NotZero(services.ServiceProvider.GetRequiredService<ITaskDbContext>().Tasks.Count());
    }
}
