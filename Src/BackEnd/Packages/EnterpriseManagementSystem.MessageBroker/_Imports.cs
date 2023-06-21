// Global using directives

global using EnterpriseManagementSystem.MessageBroker.Abstractions;
global using EnterpriseManagementSystem.MessageBroker.Implementations;
global using EnterpriseManagementSystem.MessageBroker.Options;
global using MassTransit;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Bus = EnterpriseManagementSystem.MessageBroker.Implementations.Bus;
global using IBus = EnterpriseManagementSystem.MessageBroker.Abstractions.IBus;