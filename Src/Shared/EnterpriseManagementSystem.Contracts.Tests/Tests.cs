using EnterpriseManagementSystem.BusinessModels;
using EnterpriseManagementSystem.Contracts.Common;
using EnterpriseManagementSystem.Contracts.WebContracts;

namespace EnterpriseManagementSystem.Contracts.Tests;

public sealed class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void DtoEqualsTest()
    {
        var taskGuid = Guid.NewGuid();
        var executorGuid = Guid.NewGuid();

        var data = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin")));

        var data2 = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin")));


        Assert.That(data, Is.EqualTo(data2));
    }

    [Test]
    public void ToJson_Test()
    {
        var taskGuid = Guid.NewGuid();
        var executorGuid = Guid.NewGuid();

        var data = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin")));


        Assert.Pass(data.ToJson());
    }
    
    [Test]
    public void ToModel_Test()
    {
        var taskGuid = Guid.NewGuid();
        var executorGuid = Guid.NewGuid();

        var data = new TaskInfo(taskGuid, "Update test name", "Update test desc", "Registred",
            new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin"),
            Observers: new RecordsCollection<Account>(new Account(executorGuid, EmailAddress.Parse("admin@admin.com"), "admin", "admin")));

        var json = data.ToJson();
        
        Assert.DoesNotThrow(() => data.ToModel<TaskInfo>(json));
    }
}
