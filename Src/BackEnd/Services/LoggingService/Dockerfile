FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/BackEnd/Services/LoggingService/LoggingService.csproj", "Src/BackEnd/Services/LoggingService/"]
RUN dotnet restore "Src/BackEnd/Services/LoggingService/LoggingService.csproj"
COPY . .
WORKDIR "/src/Src/BackEnd/Services/LoggingService"
RUN dotnet build "LoggingService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LoggingService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoggingService.dll"]
