namespace EnterpriseManagementSystem.BusinessModels.Tests;

public class EmailTests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void TryParse_SuccessScenario()
    {
        Assert.DoesNotThrow(() => EmailAddress.TryParse("admin@google.com"));
    }
    
    [Test]
    public void TryParse_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => EmailAddress.TryParse(null));
    }
    
    [Test]
    public void TryParse_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => EmailAddress.TryParse("adm$in@goo$gle.com"));
    }
}