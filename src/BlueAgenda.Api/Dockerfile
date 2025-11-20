# Estágio 1: Base (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# EXPOSE 8080 (Esta porta é para a aplicação HTTP/HTTPS, o debug usa outra, que será configurada no docker-compose)

# Estágio 2: Build (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura (para cache)
COPY ["src/BlueAgenda.Api/BlueAgenda.Api.csproj", "src/BlueAgenda.Api/"]
# Se você tiver outras referências de projeto, deve copiar os csproj delas também e rodar o restore no SLN
# Exemplo (se fosse rodar restore no nível da solução): RUN dotnet restore BlueAgenda.sln 
# Já que você só copiou o csproj da API, vamos manter o restore só nela por agora:
RUN dotnet restore "src/BlueAgenda.Api/BlueAgenda.Api.csproj"

# Copia todo o código da solução
COPY . . 

# Publica o projeto
RUN dotnet publish "src/BlueAgenda.Api/BlueAgenda.Api.csproj" -c Release -o /app/publish --no-restore

# Estágio Final: Aplicação
FROM base AS final
WORKDIR /app
# Copia o output da pasta publish
COPY --from=build /app/publish .

# O ENTRYPOINT padrão será usado e modificado pelo docker-compose.yml para debug
# ENTRYPOINT ["dotnet", "BlueAgenda.Api.dll"]