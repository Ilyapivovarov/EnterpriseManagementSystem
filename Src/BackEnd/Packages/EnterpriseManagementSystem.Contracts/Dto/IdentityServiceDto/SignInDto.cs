namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SignInDto(
        [property: JsonConverter(typeof(EmailAddressJsonConverter))]
        EmailAddress Email,
        [property: JsonConverter(typeof(PasswordJsonConverter))]
        Password Password)
    : ContractBase;