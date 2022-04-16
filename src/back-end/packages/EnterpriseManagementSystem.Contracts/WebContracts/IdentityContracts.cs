namespace EnterpriseManagementSystem.Contracts.WebContracts;

public record SignIn(string Email, string Password);

public record SignUpDto(string Email, string Password, string ConfirmPassword);

public record Account(int Id, Guid Guid, string Email, string FirstName, string LastName, string Role);

public record Session(string AccessToken, string RefreshToken, Guid UserGuid);