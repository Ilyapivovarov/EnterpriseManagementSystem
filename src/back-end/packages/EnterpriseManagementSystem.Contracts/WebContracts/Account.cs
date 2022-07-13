namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record Account(Guid Guid, string Email, string FirstName, string LastName) : ContractBase;