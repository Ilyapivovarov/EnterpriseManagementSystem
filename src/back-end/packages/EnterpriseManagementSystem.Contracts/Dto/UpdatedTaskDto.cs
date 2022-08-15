namespace EnterpriseManagementSystem.Contracts.Dto;

public record UpdatedTaskDto(int Id, Guid Guid, string Name, string? Description) : ContractBase;
