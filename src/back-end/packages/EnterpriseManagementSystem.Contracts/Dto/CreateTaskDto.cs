namespace EnterpriseManagementSystem.Contracts.Dto;

public record CreateTaskDto(string Name, string? Description, int AuthorId, int StatusId, int? ExecutorId, int? inspectorId);