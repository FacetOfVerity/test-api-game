FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY Tests Tests/
COPY Shared Shared/

WORKDIR /src/Tests/BattleRoom.Tests.End2End.Launcher/
RUN dotnet publish BattleRoom.Tests.End2End.Launcher.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BattleRoom.Tests.End2End.Launcher.dll"]


