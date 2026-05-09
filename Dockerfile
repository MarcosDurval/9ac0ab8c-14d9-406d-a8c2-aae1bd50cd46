FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY api/src/OrcamentosApi.csproj api/src/
RUN dotnet restore api/src/OrcamentosApi.csproj

COPY api/src/ api/src/
RUN dotnet publish api/src/OrcamentosApi.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrcamentosApi.dll"]
