using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UserService.Application.DbContexts;
using UserService.FunctionalTests.Base;
using UserService.Infrastructure.Mapper;

namespace UserService.FunctionalTests.Consumers;

public sealed class SaveNewUserConsumerScenarioTest : TestBase
{
    protected override string Environment => "Testing";

    private HttpClient HttpClient { get; set; } = null!;

    [SetUp]
    public async Task SetUp()
    {
        HttpClient = await GetHttpClient();
    }

    [Test]
    public async Task SuccessScenarion()
    {
        var bus = Services.ServiceProvider.GetRequiredService<IBus>();
        var userData = new UserDataResponse(Guid.NewGuid(), "Test", "Test", EmailAddress.Parse("admin@admin.com"), DateTime.Today);

        var @event = new SignUpUserIntegrationEvent(userData);
        await bus.Publish(@event);

        await Task.Delay(100);

        var userDbContext = Services.ServiceProvider.GetRequiredService<IUserDbContext>();
        Assert.AreEqual(userData, userDbContext.Users.OrderBy(x => x.Created).Last().ToDto());
    }
}