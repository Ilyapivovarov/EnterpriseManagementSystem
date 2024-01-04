namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record UserInfo(Guid Guid, string? FirstName, string? LastName, string? Role) : ContractBase;