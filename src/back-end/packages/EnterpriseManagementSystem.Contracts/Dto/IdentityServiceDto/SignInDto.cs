namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SignInDto([property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress Email, string Password) 
    : ContractBase;

