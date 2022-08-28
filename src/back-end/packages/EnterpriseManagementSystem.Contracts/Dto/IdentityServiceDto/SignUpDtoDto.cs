namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SignUpDtoDto
    (string FirstName, string LastName, string Email, string Password, string ConfirmPassword) : ContractBase;