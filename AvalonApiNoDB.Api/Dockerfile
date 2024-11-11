FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AvalonApiNoDB.Api/AvalonApiNoDB.Api.csproj", "AvalonApiNoDB.Api/"]
COPY ["AvalonApiNoDB.Core/AvalonApiNoDB.Core.csproj", "AvalonApiNoDB.Core/"]
RUN dotnet restore "AvalonApiNoDB.Api/AvalonApiNoDB.Api.csproj"
COPY . .
WORKDIR "/src/AvalonApiNoDB.Api"
RUN dotnet build "AvalonApiNoDB.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AvalonApiNoDB.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvalonApiNoDB.Api.dll"]
