# ===========================
# STAGE 1 - Build da aplicação
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY *.sln .
COPY SchoolManager.API/*.csproj SchoolManager.API/
COPY SchoolManager.Application/*.csproj SchoolManager.Application/
COPY SchoolManager.Domain/*.csproj SchoolManager.Domain/
COPY SchoolManager.Infrastructure/*.csproj SchoolManager.Infrastructure/
RUN dotnet restore

# Copia o restante do código e publica a aplicação
COPY . .
WORKDIR /src/SchoolManager.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===========================
# STAGE 2 - Imagem de runtime
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia arquivos publicados do stage anterior
COPY --from=build /app/publish .

# Porta padrão do Render
EXPOSE 8080

# Define a variável obrigatória de URL
ENV ASPNETCORE_URLS=http://+:8080

# Ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "SchoolManager.API.dll"]
