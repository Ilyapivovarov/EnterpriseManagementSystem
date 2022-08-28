namespace EnterpriseManagementSystem.Contracts.WebContracts.Base;

public abstract record ContractBase
{
    protected readonly static JsonSerializerOptions JsonOpt = new()
    {
        WriteIndented = true
    };
    
    
    public virtual string ToJson()
    {
        return JsonSerializer.Serialize<object>(this, JsonOpt);
    }   
}