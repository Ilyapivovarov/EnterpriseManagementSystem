namespace EnterpriseManagementSystem.Contracts.WebContracts;

public readonly record struct NewTask(string Name, string Description, string StatusName, Guid author,
    Guid? Executor = null,
    Guid? Inspector = null,
    Guid[]? Observers = null);

public readonly record struct TaskInfo(string Name, string Description, Account author, Account? Executor,
    Account? Inspector,
    Account[]? Observers, string StatusName);