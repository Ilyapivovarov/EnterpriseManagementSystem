namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record NewTask(string Name, string Description, string StatusName, Guid author,
    Guid? Executor = null,
    Guid? Inspector = null,
    ICollection<Guid>? Observers = null);

public sealed record TaskInfo(Guid Guid, string Name, string Description, string StatusName, Account Author,
    Account? Executor = null,
    Account? Inspector = null,
    ICollection<Account>? Observers = null);

public sealed record UpdateTask(Guid Guid, string? Name = null, string? Description = null, string? StatusName = null,
    Guid? Executor = null,
    Guid? Inspector = null,
    ICollection<Guid>? Observers = null);