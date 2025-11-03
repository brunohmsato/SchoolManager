# ===========================
# STAGE 1 - Build da aplicação
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solução e os projetos da pasta SchoolManager/
COPY SchoolManager/SchoolManager.slnx ./
COPY SchoolManager/SchoolManager.API/*.csproj SchoolManager.API/
COPY SchoolManager/SchoolManager.Application/*.csproj SchoolManager.Application/
COPY SchoolManager/SchoolManager.Domain/*.csproj SchoolManager.Domain/
COPY SchoolManager/SchoolManager.Infrastructure/*.csproj SchoolManager.Infrastructure/

# Restaura as dependências
RUN dotnet restore SchoolManager.slnx

# Copia o restante do código
COPY SchoolManager/. .

# Publica a API
WORKDIR /src/SchoolManager.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===========================
# STAGE 2 - Runtime
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "SchoolManager.API.dll"]
