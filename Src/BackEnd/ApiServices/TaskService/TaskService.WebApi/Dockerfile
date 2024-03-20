#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/ApiServices/TaskService/TaskService.WebApi/TaskService.WebApi.csproj", "Src/BackEnd/ApiServices/TaskService/TaskService.WebApi/"]
COPY ["Src/BackEnd/ApiServices/TaskService/TaskService.Infrastructure/TaskService.Infrastructure.csproj", "Src/BackEnd/ApiServices/TaskService/TaskService.Infrastructure/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Shared/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Shared/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Shared/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Shared/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Shared/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/Shared/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/Shared/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/Shared/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["Src/BackEnd/ApiServices/TaskService/TaskService.Application/TaskService.Application.csproj", "Src/BackEnd/ApiServices/TaskService/TaskService.Application/"]
COPY ["Src/BackEnd/ApiServices/TaskService/TaskService.Core/TaskService.Core.csproj", "Src/BackEnd/ApiServices/TaskService/TaskService.Core/"]
RUN dotnet restore "Src/BackEnd/ApiServices/TaskService/TaskService.WebApi/TaskService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/ApiServices/TaskService/TaskService.WebApi"
RUN dotnet build "TaskService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.WebApi.dll"]
