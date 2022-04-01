using ApiGateway.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Application.ServiceInterfaces;

public interface IAuthHttpClientService
{
    public Task<IActionResult> SignInAsync(SignInDto signIn);

    public Task<IActionResult> SignUpAsync(SignUpDto signUpDto);
}