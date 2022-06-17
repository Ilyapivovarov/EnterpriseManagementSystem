using EnterpriseManagementSystem.Contracts.WebContracts.Base;

namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignIn(string Email, string Password) : ContractBase;

public sealed record SignUp
    (string FristName, string LastName, string Email, string Password, string ConfirmPassword) : ContractBase;

public sealed record Session(string AccessToken, Guid RefreshToken, Guid UserGuid) : ContractBase;

public sealed record Account(Guid Guid, string Email, string FirstName, string LastName, string Role) : ContractBase;

public sealed record UserInfo(Guid Guid, string? FirstName, string? LastName, string? Role) : ContractBase;