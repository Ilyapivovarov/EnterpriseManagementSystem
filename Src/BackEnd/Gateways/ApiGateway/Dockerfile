#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Gateways/ApiGateway/ApiGateway.csproj", "Src/BackEnd/Gateways/ApiGateway/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Shared/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Shared/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Shared/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Shared/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Shared/EnterpriseManagementSystem.MessageBroker/"]
COPY ["Src/Shared/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "Src/Shared/EnterpriseManagementSystem.JwtAuthorization/"]
RUN dotnet restore "Src/BackEnd/Gateways/ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Gateways/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
