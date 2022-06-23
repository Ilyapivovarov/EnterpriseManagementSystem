namespace EnterpriseManagementSystem.Contracts.WebContracts;

public sealed record SignUp
    (string FristName, string LastName, string Email, string Password, string ConfirmPassword) : ContractBase;