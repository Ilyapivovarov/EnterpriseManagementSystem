namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SignUpDtoDto
    (string FirstName, string LastName, [property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress Email, string Password, string ConfirmPassword) : ContractBase;