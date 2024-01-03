#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Services/TaskService/TaskService.WebApi/TaskService.WebApi.csproj", "Src/BackEnd/Services/TaskService/TaskService.WebApi/"]
COPY ["Src/BackEnd/Services/TaskService/TaskService.Infrastructure/TaskService.Infrastructure.csproj", "Src/BackEnd/Services/TaskService/TaskService.Infrastructure/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["Src/BackEnd/Services/TaskService/TaskService.Application/TaskService.Application.csproj", "Src/BackEnd/Services/TaskService/TaskService.Application/"]
COPY ["Src/BackEnd/Services/TaskService/TaskService.Core/TaskService.Core.csproj", "Src/BackEnd/Services/TaskService/TaskService.Core/"]
RUN dotnet restore "Src/BackEnd/Services/TaskService/TaskService.WebApi/TaskService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Services/TaskService/TaskService.WebApi"
RUN dotnet build "TaskService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.WebApi.dll"]
