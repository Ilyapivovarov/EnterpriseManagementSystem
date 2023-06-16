#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/back-end/gateways/ApiGateway/ApiGateway.csproj", "src/back-end/gateways/ApiGateway/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "src/back-end/packages/EnterpriseManagementSystem.Contracts/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "src/back-end/packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "src/back-end/packages/EnterpriseManagementSystem.MessageBroker/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/"]
RUN dotnet restore "src/back-end/gateways/ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/back-end/gateways/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]