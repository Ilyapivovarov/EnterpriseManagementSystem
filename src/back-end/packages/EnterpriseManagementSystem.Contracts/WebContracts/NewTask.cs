using EnterpriseManagementSystem.Contracts.Common;

namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record NewTask(string Name, string Description, string StatusName, Guid Author,
    Guid? Executor = null,
    Guid? Inspector = null,
    RecordsCollection<Guid>? Observers = null) : ContractBase;