using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Application.Mediators.Requests;

public class Request<T> : IRequest<IActionResult>
{
    public Request(T? value)
    {
        Body = value;
    }
    
    public T? Body { get; }

}