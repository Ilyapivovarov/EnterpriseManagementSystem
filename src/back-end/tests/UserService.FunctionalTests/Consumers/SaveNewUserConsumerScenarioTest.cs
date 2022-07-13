using System;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
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
        var userData = new UserDataResponse(Guid.NewGuid(), "Test", "Test", "Test@address.com", DateTime.Today);

        var @event = new SignUpUserIntegrationEvent(userData);
        await bus.Publish(@event);

        await Task.Delay(2000);

        Assert.AreEqual(userData, UserDbContext.Users.Last().ToDto());

    }
}