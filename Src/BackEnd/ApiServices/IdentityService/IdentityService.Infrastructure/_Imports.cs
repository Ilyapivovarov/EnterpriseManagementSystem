﻿// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using AutoMapper;
global using EnterpriseManagementSystem.BusinessModels;
global using EnterpriseManagementSystem.Cache;
global using EnterpriseManagementSystem.Cache.Abstractions;
global using EnterpriseManagementSystem.Contracts.Dto.IdentityServiceDto;
global using EnterpriseManagementSystem.Contracts.WebContracts;
global using EnterpriseManagementSystem.Helpers.Extensions;
global using EnterpriseManagementSystem.JwtAuthorization;
global using EnterpriseManagementSystem.Logging;
global using EnterpriseManagementSystem.MessageBroker;
global using EnterpriseManagementSystem.MessageBroker.Abstractions;
global using IdentityService.Application.DbContexts;
global using IdentityService.Application.Repositories;
global using IdentityService.Application.Services;
global using IdentityService.Core;
global using IdentityService.Core.DbEntities;
global using IdentityService.Infrastructure.DbContexts;
global using IdentityService.Infrastructure.HostedServices;
global using IdentityService.Infrastructure.Repositories;
global using IdentityService.Infrastructure.Repositories.Base;
global using IdentityService.Infrastructure.Requests;
global using IdentityService.Infrastructure.Services;
global using MediatR;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;