// Global using directives

global using AutoMapper;
global using EnterpriseManagementSystem.BusinessModels;
global using EnterpriseManagementSystem.Contracts.IntegrationEvents;
global using EnterpriseManagementSystem.Contracts.WebContracts.Request;
global using EnterpriseManagementSystem.Contracts.WebContracts.Response;
global using EnterpriseManagementSystem.MessageBroker.Abstractions;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using UserService.Application.DbContexts;
global using UserService.Application.Repository;
global using UserService.Application.Services;
global using UserService.Core.DbEntities;
global using UserService.Infrastructure.Handlers.Base;
global using UserService.Infrastructure.Mapper;
global using UserService.Infrastructure.Request;