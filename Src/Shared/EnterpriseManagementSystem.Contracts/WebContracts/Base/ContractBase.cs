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
    
    public virtual T ToModel<T>(string json)
    {
        var model = JsonSerializer.Deserialize<T>(json);

        if (model is null)
        {
            throw new ArgumentNullException($"Intput json can not deserialize to model: {typeof(T).Name}");
        }

        return model;
    }
}