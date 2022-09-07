using System.Text.RegularExpressions;

namespace EnterpriseManagementSystem.BusinessModels;

public readonly record struct EmailAddress : IComparable<EmailAddress>
{
    public static EmailAddress Parse(string? value) => new(value);

    public static bool TryParse(string? value, out EmailAddress emailAddress)
    {
        try
        {
            emailAddress = Parse(value);
            return true;
        }
        catch
        {
            emailAddress = default;
            return false;
        }
    }

    private EmailAddress(string? value)
    {
        Value = ValidateValueOrException(value);
    }

    public string Value { get; }

    private static string ValidateValueOrException(string? value)
    {
        if (value == null)
            throw new ArgumentNullException(value);

        var normalizeValue = NormalizeValue(value);

        var mask = new Regex(
            @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
        var isSucces = mask.IsMatch(normalizeValue);

        if (!isSucces)
            throw new ArgumentException(value);

        return normalizeValue;
    }

    private static string NormalizeValue(string value) => value.ToLower().Trim();

    public int CompareTo(EmailAddress other)
    {
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}
