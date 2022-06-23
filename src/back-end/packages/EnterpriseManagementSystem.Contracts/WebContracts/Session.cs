namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record Session(string AccessToken, Guid RefreshToken, Guid UserGuid) : ContractBase;