#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Services/UserService/UserService.WebApi/UserService.WebApi.csproj", "Src/BackEnd/Services/UserService/UserService.WebApi/"]
COPY ["Src/BackEnd/Services/UserService/UserService.Application/UserService.Application.csproj", "Src/BackEnd/Services/UserService/UserService.Application/"]
COPY ["Src/BackEnd/Services/UserService/UserService.Core/UserService.Core.csproj", "Src/BackEnd/Services/UserService/UserService.Core/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/BackEnd/Services/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "Src/BackEnd/Services/UserService/UserService.Infrastructure/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/EnterpriseManagementSystem.Helpers.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.Helpers/"]
COPY ["Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/BackEnd/Packages/EnterpriseManagementSystem.JwtAuthorization/"]
RUN dotnet restore "Src/BackEnd/Services/UserService/UserService.WebApi/UserService.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Services/UserService/UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]