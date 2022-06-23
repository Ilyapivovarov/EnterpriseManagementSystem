using EnterpriseManagementSystem.Contracts.Common;

namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record UpdateTask(Guid Guid, string? Name = null, string? Description = null, string? StatusName = null,
    Guid? Executor = null,
    Guid? Inspector = null,
    RecordsCollection<Guid>? Observers = null) : ContractBase;