# Usa la imagen oficial de .NET 10 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia los archivos del proyecto y restaura dependencias
COPY ["LicenciasAPI.csproj", "./"]
RUN dotnet restore "LicenciasAPI.csproj"

# Copia el resto del código y compila
COPY . .
RUN dotnet publish "LicenciasAPI.csproj" -c Release -o /app/publish

# Genera la imagen final de ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Render usa el puerto 10000 por defecto para Docker
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "LicenciasAPI.dll"]