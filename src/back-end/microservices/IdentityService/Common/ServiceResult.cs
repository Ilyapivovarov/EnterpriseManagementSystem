namespace IdentityService.Common;

public readonly struct ServiceResult<TValue>
{
    public static ServiceResult<TValue> CreateSuccessResult(TValue value) => new(value);
    
    public static ServiceResult<TValue> CreateUnsuccessfulResult(string value) => new(value);
    
    private ServiceResult(TValue value)
    {
        Value = value;
        Error = default;
    }
    
    private ServiceResult(string error)
    {
        Error = error;
        Value = default;
    }

    public TValue? Value { get; }
    
    public string? Error { get; }

}