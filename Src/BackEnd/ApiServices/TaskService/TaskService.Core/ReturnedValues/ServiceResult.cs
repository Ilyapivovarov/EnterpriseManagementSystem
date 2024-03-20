namespace TaskService.Core.ReturnedValues;

public readonly struct ServiceResult<T>
{
    public ServiceResult(T value)
    {
        Value = value;
        Error = default;
    }

    public ServiceResult(string error)
    {
        Error = error;
        Value = default;
    }

    public T? Value { get; }

    public string? Error { get; }

    public bool HasError => !string.IsNullOrWhiteSpace(Error);
}
