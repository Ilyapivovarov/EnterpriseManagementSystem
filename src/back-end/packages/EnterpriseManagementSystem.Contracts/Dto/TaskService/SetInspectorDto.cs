namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public sealed record SetInspectorDto(int TaskId, int? InspectorId) : ContractBase;