namespace EnterpriseManagementSystem.BusinessModels.Tests;

public class EmailTests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void TryParse_SuccessScenario()
    {
        Assert.DoesNotThrow(() => Email.TryParse("admin@google.com"));
    }
    
    [Test]
    public void TryParse_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Email.TryParse(null));
    }
    
    [Test]
    public void TryParse_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Email.TryParse("adm$in@goo$gle.com"));
    }
}