namespace EnterpriseManagementSystem.Contracts.WebContracts;

public readonly record struct NewTask(string Name, string Description, string StatusName, Guid author,
    Guid? Executor = null,
    Guid? Inspector = null,
    Guid[]? Observers = null);

public readonly record struct TaskInfo(Guid Guid, string Name, string Description, string StatusName, Account Author,
    Account? Executor = null,
    Account? Inspector = null,
    Account[]? Observers = null);