using System.Text.RegularExpressions;

namespace EnterpriseManagementSystem.BusinessModels;

public class EmailAddress
{
    public static EmailAddress TryParse(string? value)
    {
        return new(value);
    }

    private EmailAddress(string? value)
    {
        Value = ValidateOrException(value);
    }

    public string Value { get; }

    private string ValidateOrException(string? value)
    {
        if (value == null)
            throw new ArgumentNullException(value);

        var mask = new Regex(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
        var isSucces = mask.IsMatch(value);

        if (!isSucces)
            throw new ArgumentException(value);

        return value;
    }
}
