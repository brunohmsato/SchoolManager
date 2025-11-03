# ===========================
# STAGE 1 - Build da aplicação
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY *.sln .
COPY API/*.csproj API/
COPY Application/*.csproj Application/
COPY Domain/*.csproj Domain/
COPY Infrastructure/*.csproj Infrastructure/
RUN dotnet restore

# Copia o restante do código e publica a aplicação
COPY . .
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===========================
# STAGE 2 - Imagem de runtime
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia arquivos publicados do stage anterior
COPY --from=build /app/publish .

# Define a porta padrão exposta (Render detecta automaticamente)
EXPOSE 8080

# Variável obrigatória para ASP.NET em ambiente Linux
ENV ASPNETCORE_URLS=http://+:8080

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "API.dll"]
