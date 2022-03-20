namespace IdentityService.Dto;

public record SignInDto(string Email, string Password);

public record SignOnDto(string Email, string Password, string ConfirmPassword);