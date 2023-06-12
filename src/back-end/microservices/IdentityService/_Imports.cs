global using AutoMapper;
global using EnterpriseManagementSystem.BusinessModels;
global using EnterpriseManagementSystem.Contracts;
global using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
global using EnterpriseManagementSystem.Contracts.IntegrationEvents;
global using EnterpriseManagementSystem.Contracts.WebContracts;
global using EnterpriseManagementSystem.Contracts.WebContracts.Response;
global using EnterpriseManagementSystem.JwtAuthorization;
global using IdentityService;
global using IdentityService.Application;
global using IdentityService.Application.DbContexts;
global using IdentityService.Application.Repositories;
global using IdentityService.Application.Services;
global using IdentityService.Core.DbEntities;
global using IdentityService.Core.DbEntities.Base;
global using IdentityService.Core.ReturnedValue;
global using IdentityService.Infrastructure;
global using IdentityService.Infrastructure.DbContexts;
global using IdentityService.Infrastructure.Extensions;
global using IdentityService.Infrastructure.Mapper;
global using IdentityService.Infrastructure.Repositories.Base;
global using IdentityService.Infrastructure.Requests;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;