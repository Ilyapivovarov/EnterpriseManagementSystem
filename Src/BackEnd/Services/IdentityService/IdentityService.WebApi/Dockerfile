#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.WebApi/IdentityService.WebApi.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.WebApi/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Shared/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Shared/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Shared/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Shared/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Shared/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.Infrastructure/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Cache/EnterpriseManagementSystem.Cache.csproj", "Src/Shared/EnterpriseManagementSystem.Cache/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/Shared/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/Shared/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/Shared/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Logging/EnterpriseManagementSystem.Logging.csproj", "Src/Shared/EnterpriseManagementSystem.Logging/"]
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.Application/IdentityService.Application.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.Application/"]
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.Core/IdentityService.Core.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.Core/"]
RUN dotnet restore "Src/BackEnd/Services/IdentityService/IdentityService.WebApi/IdentityService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Services/IdentityService/IdentityService.WebApi"
RUN dotnet build "IdentityService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.WebApi.dll"]
