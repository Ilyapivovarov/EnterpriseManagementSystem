using ApiGateway.Dto;

namespace ApiGateway.Application.ServiceInterfaces;

public interface IAuthHttpClientService
{
    public Task<SessionDto> SignInAsync(SignInDto signIn);

    public Task<SessionDto> SignUpAsync(SignUpDto signUpDto);
}