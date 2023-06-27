FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/MicroservicesV2/UserService/UserService.WebApi/UserService.WebApi.csproj", "Src/BackEnd/MicroservicesV2/UserService/UserService.WebApi/"]
COPY ["Src/BackEnd/MicroservicesV2/UserService/UserService.Core/UserService.Core.csproj", "Src/BackEnd/MicroservicesV2/UserService/UserService.Core/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/BackEnd/MicroservicesV2/UserService/UserService.Application/UserService.Application.csproj", "Src/BackEnd/MicroservicesV2/UserService/UserService.Application/"]
COPY ["Src/BackEnd/MicroservicesV2/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "Src/BackEnd/MicroservicesV2/UserService/UserService.Infrastructure/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/"]
RUN dotnet restore "Src/BackEnd/MicroservicesV2/UserService/UserService.WebApi/UserService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/MicroservicesV2/UserService/UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]
