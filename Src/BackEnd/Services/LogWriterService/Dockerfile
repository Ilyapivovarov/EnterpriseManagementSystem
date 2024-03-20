#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Src/BackEnd/Services/LogWriterService/LogWriterService.csproj", "Src/BackEnd/Services/LogWriterService/"]
COPY ["Src/Shared/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "Src/Shared/EnterpriseManagementSystem.Contracts/"]
COPY ["Src/Shared/EnterpriseManagementSystem.BusinessModels/EnterpriseManagementSystem.BusinessModels.csproj", "Src/Shared/EnterpriseManagementSystem.BusinessModels/"]
COPY ["Src/Shared/EnterpriseManagementSystem.MessageBroker/EnterpriseManagementSystem.MessageBroker.csproj", "Src/Shared/EnterpriseManagementSystem.MessageBroker/"]
RUN dotnet restore "Src/BackEnd/Services/LogWriterService/LogWriterService.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Services/LogWriterService"
RUN dotnet build "LogWriterService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogWriterService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogWriterService.dll"]
