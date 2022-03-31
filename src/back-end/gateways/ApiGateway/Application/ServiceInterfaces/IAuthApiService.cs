using ApiGateway.Dto;

namespace ApiGateway.Application.ServiceInterfaces;

public interface IAuthApiService
{
    public Task<SessionDto> SignInAsync(SignInDto signIn);

    public Task<SessionDto> SignUpAsync(SignUpDto signUpDto);
}