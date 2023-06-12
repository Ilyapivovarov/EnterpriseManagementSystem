#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/back-end/microservices/TaskService/TaskService.csproj", "src/back-end/microservices/TaskService/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "src/back-end/packages/EnterpriseManagementSystem.Contracts/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "src/back-end/packages/EnterpriseManagementSystem.MessageBroker/"]

RUN dotnet restore "src/back-end/microservices/TaskService/TaskService.csproj"
COPY . .
WORKDIR "/src/src/back-end/microservices/TaskService"
RUN dotnet build "TaskService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.dll"]