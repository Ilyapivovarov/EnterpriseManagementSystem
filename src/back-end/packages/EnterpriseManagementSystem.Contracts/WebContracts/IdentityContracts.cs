namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignIn(string Email, string Password);

public sealed record SignUp(string FristName, string LastName, string Email, string Password, string ConfirmPassword);

public sealed record Session(string AccessToken, Guid RefreshToken, Guid UserGuid);

public sealed record Account(Guid Guid, string Email, string FirstName, string LastName, string Role);

public sealed record UserInfo(Guid Guid, string? FirstName, string? LastName, string? Role);