#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["Src/FrontEnd/ReactApp/ReactApp.csproj", "Src/FrontEnd/ReactApp/"]
RUN dotnet restore "Src/FrontEnd/ReactApp/ReactApp.csproj"
COPY . .
WORKDIR "/src/Src/FrontEnd/ReactApp"
RUN dotnet build "ReactApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReactApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReactApp.dll"]