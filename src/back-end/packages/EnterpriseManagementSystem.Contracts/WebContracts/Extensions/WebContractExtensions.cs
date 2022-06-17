using EnterpriseManagementSystem.Contracts.WebContracts.Base;
using Newtonsoft.Json;

namespace EnterpriseManagementSystem.Contracts.WebContracts.Extensions;

public static class WebContractExtensions
{
    public static string ToJson(this ContractBase webContract)
    {
        return JsonConvert.SerializeObject(webContract);
    }
}