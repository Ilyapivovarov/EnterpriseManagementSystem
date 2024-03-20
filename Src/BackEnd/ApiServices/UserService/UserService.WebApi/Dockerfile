#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/ApiServices/UserService/UserService.WebApi/UserService.WebApi.csproj", "Src/BackEnd/ApiServices/UserService/UserService.WebApi/"]
COPY ["Src/BackEnd/ApiServices/UserService/UserService.Application/UserService.Application.csproj", "Src/BackEnd/ApiServices/UserService/UserService.Application/"]
COPY ["Src/BackEnd/ApiServices/UserService/UserService.Core/UserService.Core.csproj", "Src/BackEnd/ApiServices/UserService/UserService.Core/"]
COPY ["Src/Shared/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Shared/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/BackEnd/ApiServices/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "Src/BackEnd/ApiServices/UserService/UserService.Infrastructure/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Shared/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Shared/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Shared/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/Shared/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/Shared/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/Shared/EnterpriseManagementSystem.JwtAuthorization/"]
RUN dotnet restore "Src/BackEnd/ApiServices/UserService/UserService.WebApi/UserService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/ApiServices/UserService/UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]
