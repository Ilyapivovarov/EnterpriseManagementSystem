namespace IdentityService.Dto;

public record SignInDto(string Email, string Password);

public record SignUpDto(string Email, string Password, string ConfirmPassword);