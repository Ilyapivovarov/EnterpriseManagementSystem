#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/back-end/microservices/EmailService/EmailService.csproj", "src/back-end/microservices/EmailService/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/EnterpriseManagementSystem.JwtAuthorization.csproj", "src/back-end/packages/EnterpriseManagementSystem.JwtAuthorization/"]
COPY ["src/back-end/packages/EnterpriseManagementSystem.Contracts/EnterpriseManagementSystem.Contracts.csproj", "src/back-end/packages/EnterpriseManagementSystem.Contracts/"]
RUN dotnet restore "src/back-end/microservices/EmailService/EmailService.csproj"
COPY . .
WORKDIR "/src/src/back-end/microservices/EmailService"
RUN dotnet build "EmailService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailService.dll"]