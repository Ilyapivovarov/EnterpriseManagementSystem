namespace IdentityService.Common;

public class ServiceResult<TValue>
{
    public static ServiceResult<TValue> CreateSuccessResult(TValue value) => new(value);
    
    public static ServiceResult<TValue> CreateUnsuccessfulResult(string value) => new(value);
    
    private ServiceResult(TValue value)
    {
        Value = value;
    }
    
    private ServiceResult(string error)
    {
        Error = error;
    }

    public TValue? Value { get; }
    
    public string? Error { get; }

    public bool IsSuccess => Value is not null;
    
}