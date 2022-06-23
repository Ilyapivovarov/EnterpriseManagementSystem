namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignIn(string Email, string Password) : ContractBase;