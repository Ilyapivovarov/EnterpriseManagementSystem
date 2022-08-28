namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignIn([property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress Email, string Password) 
    : ContractBase;

