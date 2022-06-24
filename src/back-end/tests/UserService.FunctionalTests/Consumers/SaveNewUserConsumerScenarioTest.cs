using System;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UserService.FunctionalTests.Base;

namespace UserService.FunctionalTests.Consumers;

public sealed class SaveNewUserConsumerScenarioTest : TestBase
{
    private IBus Bus { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        RefreshServer();
        Bus = Server.Services.GetRequiredService<IBus>();
    }

    [Test]
    public async Task SuccessScenarion()
    {
        var account = new Account(Guid.NewGuid(), "test@test.com", "Test", "Test", "Test");
        var @event = new SignUpUserIntegrationEvent(account);

        await Bus.Publish(@event);

        await Task.Delay(1000);

        Assert.AreEqual(account.Guid, TaskDbContext.Users.Last().IdentityGuid);

    }
}