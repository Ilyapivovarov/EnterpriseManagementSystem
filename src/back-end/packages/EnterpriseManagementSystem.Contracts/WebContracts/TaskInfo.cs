using EnterpriseManagementSystem.Contracts.Common;

namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record TaskInfo(Guid Guid, string Name, string? Description, string StatusName, Account Author,
    Account? Executor = null,
    Account? Inspector = null,
    RecordsCollection<Account>? Observers = null) : ContractBase;