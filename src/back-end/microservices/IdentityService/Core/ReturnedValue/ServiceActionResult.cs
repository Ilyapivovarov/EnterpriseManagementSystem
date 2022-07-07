namespace IdentityService.Core.ReturnedValue;

public readonly struct ServiceActionResult<T>
{
    public ServiceActionResult(T value)
    {
        Value = value;
        Error = default;
    }

    public ServiceActionResult(string error)
    {
        Error = error;
        Value = default;
    }

    public T? Value { get; }

    public string? Error { get; }

    public bool HasValue => Value is not null;
}