namespace EnterpriseManagementSystem.Contracts.Dto.TaskService;

public sealed record TaskDto(int Id, Guid Guid, string Name, string? Description, DateTime Created, UserDto Author,
    UserDto? Executor, UserDto? Inspector, TaskStatusDto Status);

public sealed record TaskStatusDto(int Id, Guid Guid, string Name);

public sealed record UserDto(int Id, Guid Guid, string FirstName, string LastName, string EmailAddress);