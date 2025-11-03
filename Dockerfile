# ===========================
# STAGE 1 - Build da aplicação
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo da solução e os projetos
COPY *.sln ./
COPY SchoolManager.API/*.csproj SchoolManager.API/
COPY SchoolManager.Application/*.csproj SchoolManager.Application/
COPY SchoolManager.Domain/*.csproj SchoolManager.Domain/
COPY SchoolManager.Infrastructure/*.csproj SchoolManager.Infrastructure/

# Restaura as dependências com base na solução principal
RUN dotnet restore SchoolManager.sln

# Copia o restante do código
COPY . .

# Publica a API
WORKDIR /src/SchoolManager.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===========================
# STAGE 2 - Imagem de runtime
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia arquivos publicados do stage anterior
COPY --from=build /app/publish .

# Expõe a porta 8080 (usada pelo Render)
EXPOSE 8080

# Define a variável obrigatória para ASP.NET
ENV ASPNETCORE_URLS=http://+:8080

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "SchoolManager.API.dll"]
