FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["FinanceManager/FinanceManager.csproj", "FinanceManager/"]
COPY ["ClientApp/BlazorFinanceManager.csproj", "ClientApp/"]
RUN dotnet restore "FinanceManager/FinanceManager.csproj"
RUN dotnet restore "ClientApp/BlazorFinanceManager.csproj"

# Copiar o restante do código e compilar
COPY . .
RUN dotnet build "FinanceManager/FinanceManager.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "FinanceManager/FinanceManager.csproj" -c Release -o /app/publish

# Imagem final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Definir variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Expor porta
EXPOSE 80

# Executar a aplicação
ENTRYPOINT ["dotnet", "FinanceManager.dll"]
