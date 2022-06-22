using EnterpriseManagementSystem.Contracts.WebContracts.Base;

namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record NewTask(string Name, string Description, string StatusName, Guid Author,
    Guid? Executor = null,
    Guid? Inspector = null,
    RecordsCollection<Guid>? Observers = null) : ContractBase;

public sealed record TaskInfo(Guid Guid, string Name, string? Description, string StatusName, Account Author,
    Account? Executor = null,
    Account? Inspector = null,
    RecordsCollection<Account>? Observers = null) : ContractBase;

public sealed record UpdateTask(Guid Guid, string? Name = null, string? Description = null, string? StatusName = null,
    Guid? Executor = null,
    Guid? Inspector = null,
    RecordsCollection<Guid>? Observers = null) : ContractBase;