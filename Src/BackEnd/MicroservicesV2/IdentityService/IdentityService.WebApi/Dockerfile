#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.WebApi/IdentityService.WebApi.csproj", "Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.WebApi/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Infrastructure/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Cache/EnterpriseManagementSystem.Cache.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Cache/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Logging/EnterpriseManagementSystem.Logging.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Logging/"]
COPY ["Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Application/IdentityService.Application.csproj", "Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Application/"]
COPY ["Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Core/IdentityService.Core.csproj", "Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.Core/"]
RUN dotnet restore "Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.WebApi/IdentityService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/MicroservicesV2/IdentityService/IdentityService.WebApi"
RUN dotnet build "IdentityService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.WebApi.dll"]