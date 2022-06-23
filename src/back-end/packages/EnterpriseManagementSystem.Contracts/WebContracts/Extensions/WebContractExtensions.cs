using System.Text.Json;

namespace EnterpriseManagementSystem.Contracts.WebContracts.Extensions;

public static class WebContractExtensions
{
    private readonly static JsonSerializerOptions JsonOpt = new()
    {
        WriteIndented = true
    };
    
    public static string ToJson(this ContractBase webContract)
    {
        return JsonSerializer.Serialize<object>(webContract, JsonOpt);
    }
}