namespace EnterpriseManagementSystem.Contracts.WebContracts.Request;

public sealed record UpdateEmployeeRequest(Guid Guid, UserDataReqeust? UserData, PositionDataReqeust? Position);

public sealed record UserDataReqeust(Guid IdentityGuid, string? FirstName, string? LastName, DateTime? DataBrith);

public sealed record PositionDataReqeust(string? Name);