FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/back-end/microservices/IdentityService/IdentityService.csproj", "src/back-end/microservices/IdentityService/"]
RUN dotnet restore "src/back-end/microservices/IdentityService/IdentityService.csproj"
COPY . .
WORKDIR "/src/src/back-end/microservices/IdentityService"
RUN dotnet build "IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]