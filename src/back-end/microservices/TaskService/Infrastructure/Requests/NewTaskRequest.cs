using EnterpriseManagementSystem.Contracts.WebContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskService.Infrastructure.Requests;

public sealed class NewTaskRequest : IRequest<IActionResult>
{
    public NewTaskRequest(NewTask newTask)
    {
        NewTask = newTask;
    }
    
    public NewTask NewTask { get; }
}