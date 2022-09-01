namespace EnterpriseManagementSystem.Contracts.WebContracts.Request;

public sealed record UpdateEmployeeRequest(int Id, UserDataReqeust? UserData = null,
    PositionDataReqeust? Position = null) : ContractBase;

public sealed record UserDataReqeust(Guid IdentityGuid, string? FirstName, string? LastName, DateTime? DataBrith);

public sealed record PositionDataReqeust(string? Name);