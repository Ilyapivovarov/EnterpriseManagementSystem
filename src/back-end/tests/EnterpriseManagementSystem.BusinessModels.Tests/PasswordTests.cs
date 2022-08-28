namespace EnterpriseManagementSystem.BusinessModels.Tests;

public sealed class PasswordTests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Parse_SuccessScenario()
    {
        Assert.DoesNotThrow(() => Password.Parse("asdafsasf123"));
    }
    
    [Test]
    public void Parse_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Password.Parse(null));
    }
    
    [Test]
    public void Parse_ArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Password.Parse("123"));
    }
    
    [Test]
    public void TryParse_SuccessScenario()
    {
        var parseResult = Password.TryParse("12345", out var result);
        
        Assert.That(parseResult, Is.True, result.Value);
    }
    
    [Test]
    public void TryParse_BadScenario()
    {
        var parseResult = Password.TryParse(null, out var result);
        
        Assert.That(parseResult, Is.False, result.Value);
    }
    
    [Test]
    public void TryParse_EqualTests()
    {
        var password1 = Password.Parse("12345");
        var password2 = Password.Parse("12345");
        
        Assert.That(password1, Is.EqualTo(password2));
    }
}