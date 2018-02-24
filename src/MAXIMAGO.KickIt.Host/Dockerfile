FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY MAXIMAGO.KickIt.Host/MAXIMAGO.KickIt.Host.csproj MAXIMAGO.KickIt.Host/
COPY MAXIMAGO.KickIt.InMemoryStorage/MAXIMAGO.KickIt.InMemoryStorage.csproj MAXIMAGO.KickIt.InMemoryStorage/
COPY MAXIMAGO.KickIt/MAXIMAGO.KickIt.csproj MAXIMAGO.KickIt/
RUN dotnet restore
COPY . .
WORKDIR /src/MAXIMAGO.KickIt.Host
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MAXIMAGO.KickIt.Host.dll"]
