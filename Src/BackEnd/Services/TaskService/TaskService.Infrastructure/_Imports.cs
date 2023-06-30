// Global using directives

global using AutoMapper;
global using EnterpriseManagementSystem.Contracts.Dto.TaskService;
global using EnterpriseManagementSystem.MessageBroker.Abstractions;
global using MediatR;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using TaskService.Application.DbContexts;
global using TaskService.Application.Repositories;
global using TaskService.Application.Services;
global using TaskService.Core.DbEntities;
global using TaskService.Core.ReturnedValues;
global using TaskService.Infrastructure.DbContexts;
global using TaskService.Infrastructure.Handlers.Base;
global using TaskService.Infrastructure.Mapper;
global using TaskService.Infrastructure.Repositories.Base;
global using TaskService.Infrastructure.Requests;