FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BattleRoom.Api/BattleRoom.Api.csproj", "BattleRoom.Api/"]
RUN dotnet restore "BattleRoom.Api/BattleRoom.Api.csproj"
COPY . .
WORKDIR "/src/BattleRoom.Api"
RUN dotnet build "BattleRoom.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BattleRoom.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BattleRoom.Api.dll"]
