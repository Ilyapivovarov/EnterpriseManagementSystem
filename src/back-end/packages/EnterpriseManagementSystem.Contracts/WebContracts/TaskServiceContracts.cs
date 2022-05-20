namespace EnterpriseManagementSystem.Contracts.WebContracts;

public readonly record struct NewTask(string Name, string Description, Guid author, Guid? Executor, Guid? Inspector,
    Guid[]? Observers, string Status);