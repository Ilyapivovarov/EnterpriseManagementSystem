namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public record UpdatedTaskDto(int Id, Guid Guid, string Name, string? Description) : ContractBase;
