namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public record CreateTaskDto(string Name, string? Description, int AuthorId, int StatusId, int? ExecutorId, int? inspectorId);