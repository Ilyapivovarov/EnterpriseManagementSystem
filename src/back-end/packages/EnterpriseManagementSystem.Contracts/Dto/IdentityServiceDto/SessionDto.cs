namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SessionDto(string AccessToken, Guid RefreshToken, Guid UserGuid) : ContractBase;