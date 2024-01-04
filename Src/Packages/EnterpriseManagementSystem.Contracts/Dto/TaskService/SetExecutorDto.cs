namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public sealed record SetExecutorDto(int TaskId, Guid? ExecutorId) : ContractBase;