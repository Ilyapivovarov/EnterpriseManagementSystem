#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/WorkerServices/LogWorkerService/LogWorkerService.csproj", "Src/BackEnd/WorkerServices/LogWorkerService/"]
COPY ["Src/Packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Packages/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Packages/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Packages/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Packages/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Packages/EnterpriseManagementSystem.MessageBroker/"]
RUN dotnet restore "Src/BackEnd/WorkerServices/LogWorkerService/LogWorkerService.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/WorkerServices/LogWorkerService"
RUN dotnet build "LogWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogWorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogWorkerService.dll"]
