#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.WebApi/IdentityService.WebApi.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.WebApi/"]
COPY ["Src/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/Services/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "Src/BackEnd/Services/IdentityService/IdentityService.Infrastructure/"]
COPY ["Src/Packages/EnterpriseManagementSystem.Cache/EnterpriseManagementSystem.Cache.csproj", "Src/Packages/EnterpriseManagementSystem.Cache/"]
COPY ["Src/Packages/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/Packages/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/Packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/Packages/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["Src/Packages/EnterpriseManagementSystem.Logging/EnterpriseManagementSystem.Logging.csproj", "Src/Packages/EnterpriseManagementSystem.Logging/"]
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
