namespace EnterpriseManagementSystem.BusinessModels;

public readonly record struct Password
{
    private Password(string? value)
    {
        Value = Validate(value);
    }
    
    public string Value { get; }

    public static Password Parse(string? value) => new(value);

    public static bool TryParse(string? value, out Password result)
    {
        try
        {
            result = new Password(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
    
    private static string Validate(string? value)
    {
        if (value == null)
            throw new ArgumentNullException(value);

        if (value.Length < 5)
            throw new ArgumentException(value);

        return value;
    }
}