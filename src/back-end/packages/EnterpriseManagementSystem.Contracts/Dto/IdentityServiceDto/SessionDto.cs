namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SessionDto(string AccessToken, string RefreshToken) : ContractBase;