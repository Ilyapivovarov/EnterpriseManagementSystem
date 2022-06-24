using System;
using System.Threading.Tasks;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UserService.FunctionalTests.Base;

namespace UserService.FunctionalTests.UserController;

public sealed class SaveNewUserTest : TestBase
{
    private IBus Bus { get; set; } = null!;

    [SetUp]
    public async Task Setup()
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

        Assert.AreEqual(account.Guid, DefaultUser.Guid);
    }
}