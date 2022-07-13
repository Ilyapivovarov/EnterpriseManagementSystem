using System;
using EnterpriseManagementSystem.Contracts.Common;
using EnterpriseManagementSystem.Contracts.WebContracts;
using EnterpriseManagementSystem.Contracts.WebContracts.Extensions;
using NUnit.Framework;

namespace EnterpriseManagementSystem.Contracts.Tests;

public sealed class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Test1()
    {
        var taskGuid = Guid.NewGuid();
        var executorGuid = Guid.NewGuid();

        var data = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, "admin", "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, "admin", "admin", "admin")));

        var data2 = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, "admin", "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, "admin", "admin", "admin")));


        Assert.AreEqual(data, data2);
    }

    [Test]
    public void Test2()
    {
        var taskGuid = Guid.NewGuid();
        var executorGuid = Guid.NewGuid();

        var data = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, "admin", "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, "admin", "admin", "admin")));


        Assert.Pass(data.ToJson());
    }
}