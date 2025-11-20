FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BlueAgenda.sln .
COPY src/BlueAgenda.Api/BlueAgenda.Api.csproj src/BlueAgenda.Api/
COPY src/BlueAgenda.Application/BlueAgenda.Application.csproj src/BlueAgenda.Application/
COPY src/BlueAgenda.Domain/BlueAgenda.Domain.csproj src/BlueAgenda.Domain/
COPY src/BlueAgenda.Infrastructure/BlueAgenda.Infrastructure.csproj src/BlueAgenda.Infrastructure/

RUN dotnet restore BlueAgenda.sln

COPY . .
WORKDIR /src/src/BlueAgenda.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BlueAgenda.Api.dll"]