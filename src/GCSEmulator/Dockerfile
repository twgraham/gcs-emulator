FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/GCSEmulator/GCSEmulator.csproj", "GCSEmulator/"]
RUN dotnet restore "src/GCSEmulator/GCSEmulator.csproj"
COPY . .
WORKDIR "/src/GCSEmulator"
RUN dotnet build "GCSEmulator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GCSEmulator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GCSEmulator.dll"]
