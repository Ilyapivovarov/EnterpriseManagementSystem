namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public sealed record SignUpDtoDto(
        string FirstName,
        string LastName,
        [property: JsonConverter(typeof(EmailAddressJsonConverter))]
        EmailAddress Email,
        [property: JsonConverter(typeof(PasswordJsonConverter))]
        Password Password,
        [property: JsonConverter(typeof(PasswordJsonConverter))]
        Password ConfirmPassword
    ) : ContractBase;