namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record Account(Guid Guid, [property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress EmailAddress, string FirstName, string LastName) : ContractBase;