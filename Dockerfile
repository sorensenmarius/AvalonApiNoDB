FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

RUN dotnet restore "AvalonApiNoDB.Api/AvalonApiNoDB.Api.csproj"
WORKDIR "/src/."
COPY . .
WORKDIR "/src/AvalonApiNoDB.Api"

RUN dotnet build "AvalonApiNoDB.Api.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "AvalonApiNoDB.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvalonApiNoDB.Api.dll"]