namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record SignIn(string Email, string Password);

public record SignUp(string Email, string Password, string ConfirmPassword);

public record Session(string AccessToken, string RefreshToken, Guid UserGuid);

public record Account(Guid Guid, string EmailAddress, string FirstName, string LastName, string Role);

public record UserInfo(Guid Guid, string? FirstName, string? LastName, string? Role);