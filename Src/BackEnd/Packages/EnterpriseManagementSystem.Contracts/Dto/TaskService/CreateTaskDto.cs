namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public record CreateTaskDto(string Name, string? Description, Guid AuthorGuid, int StatusId, Guid? ExecutorId, Guid? inspectorId) 
: ContractBase;