using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.IntegrationEvents;
using EnterpriseManagementSystem.Contracts.WebContracts.Response;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TaskService.Application.DbContexts;
using TaskService.Application.Repositories;
using TaskService.FunctionalTests.Base;

namespace TaskService.FunctionalTests.DbSeedTests;

public sealed class TaskDbContextSeedTests : TestBase
{
    protected override string Environment => "Testing";

    
    [Test]
    public async Task SeedDefatulDatatTest()
    {
        var tasks = await Services.ServiceProvider.GetRequiredService<ITaskRepository>()
            .GetTasksByPage(1, 2);
        var users = await Services.ServiceProvider.GetRequiredService<IUserRepository>()
            .GetUsersByPage(0, 1);
        
        Assert.That(tasks, Is.Not.Empty);
        Assert.That(users, Is.Not.Empty);
    }
}
