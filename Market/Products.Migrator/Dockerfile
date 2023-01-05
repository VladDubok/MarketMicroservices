#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Products.Migrator/Products.Migrator.csproj", "Products.Migrator/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Products.Migrator/Products.Migrator.csproj"
COPY . .
WORKDIR "/src/Products.Migrator"
RUN dotnet build "Products.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.Migrator.dll"]