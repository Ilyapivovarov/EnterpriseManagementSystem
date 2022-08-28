namespace EnterpriseManagementSystem.Contracts.WebContracts.Response;

public sealed record EmployeeDataResponse(Guid Guid, UserDataResponse User, PositionDataResponse? Position);

public sealed record UserDataResponse(Guid IdentityGuid, string FirstName, string LastName, [property: JsonConverter(typeof(EmailAddressJsonConverter))] EmailAddress EmailAddress,
    DateTime? DataBrith);

public sealed record PositionDataResponse(string Name);