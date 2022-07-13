namespace EnterpriseManagementSystem.Contracts.WebContracts.Response;

public sealed record EmployeeDataResponse(Guid Guid, UserDataResponse UserData, PositionDataResponse? Position);

public sealed record UserDataResponse(Guid IdentityGuid, string FirstName, string LastName, string EmailAddress,
    DateTime? DataBrith);

public sealed record PositionDataResponse(string Name);