namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public sealed record SetTaskStatusDto(int TaskId, int StatusId) : ContractBase;