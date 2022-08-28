namespace EnterpriseManagementSystem.BusinessModels.Tests;

public class EmailTests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Parse_SuccessScenario()
    {
        Assert.DoesNotThrow(() => EmailAddress.Parse("admin@google.com"));
    }
    
    [Test]
    public void Parse_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => EmailAddress.Parse(null));
    }
    
    [Test]
    public void Parse_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => EmailAddress.Parse("adm$in@goo$gle.com"));
    }
    
    [Test]
    public void TryParse_SuccessScenario()
    {
        var parseResult = EmailAddress.TryParse("admin@google.com", out var result);
        
        Assert.That(parseResult, Is.True, result.Value);
    }
    
    [Test]
    public void TryParse_BadScenario()
    {
        var parseResult = EmailAddress.TryParse(null, out var result);
        
        Assert.That(parseResult, Is.False, result.Value);
    }
}