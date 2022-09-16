namespace EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;

public record IdentityUserDto(int Id, Guid Guid, 
    [property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress EmailAddress) : ContractBase;