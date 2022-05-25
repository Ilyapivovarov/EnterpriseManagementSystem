namespace EnterpriseManagementSystem.Contracts.WebContracts;

public readonly record struct NewTask(string Name, string Description, Guid author, Guid? Executor, Guid? Inspector,
    Guid[]? Observers, string StatusName);

public readonly record struct TaskInfo(string Name, string Description, Account author, Account? Executor,
    Account? Inspector,
    Account[]? Observers, string StatusName);