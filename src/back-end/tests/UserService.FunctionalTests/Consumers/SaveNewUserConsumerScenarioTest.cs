using System;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Consumers;

public sealed class SaveNewUserConsumerScenarioTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RefreshServer();
    }

    [Test]
    public async Task SuccessScenarion()
    {
        var bus = Server.Services.GetRequiredService<IBus>();
        var account = new Account(Guid.NewGuid(), "test@test.com", "Test", "Test");
        
        var @event = new SignUpUserIntegrationEvent(account);
        await bus.Publish(@event);

        await Task.Delay(1000);

        Assert.AreEqual(account, UserDbContext.Users.Last().ToDto());

    }
}