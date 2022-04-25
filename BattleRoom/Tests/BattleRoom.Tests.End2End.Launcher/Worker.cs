using BattleRoom.Client.Abstractions;
using BattleRoom.Models;
using BattleRoom.Tests.End2End.Launcher.Handlers;
using BattleRoom.Tests.End2End.Launcher.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace BattleRoom.Tests.End2End.Launcher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBattleRoomHttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IBattleRoomHttpClient httpClient, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        //Waiting for api start to work
        await Task.Delay(5000, stoppingToken);

        try
        {
            //Prepare test data
            var players = await GetOrCreatePlayers();
            var hostHandler = new LobbyActionsHandler(_logger, players.Host);
            var secondPlayerHandler = new LobbyActionsHandler(_logger, players.SecondPlayer);
            var lobbyId = Guid.NewGuid();

            //Run game
            await RunGameForHost(players.Host, hostHandler, lobbyId, stoppingToken);
            await Task.Delay(3000, stoppingToken);
            await RunGameForSecondPlayer(players.SecondPlayer, secondPlayerHandler, lobbyId, stoppingToken);

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }
    }

    private async Task RunGameForHost(PlayerDto player, LobbyActionsHandler actionsHandler, Guid lobbyId, CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var hubConnection = scope.ServiceProvider.GetService<HubConnection>();
            
        //Declare events handling
        SubscribeOnEvents(hubConnection, actionsHandler, scope);
                
        //Open connection
        await hubConnection.StartAsync(stoppingToken);
            
        //Run game
        await hubConnection.InvokeAsync("OpenTheLobby", lobbyId, player.Id, stoppingToken);
    }
    
    private async Task RunGameForSecondPlayer(PlayerDto player, LobbyActionsHandler actionsHandler, Guid lobbyId, CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var hubConnection = scope.ServiceProvider.GetService<HubConnection>();
            
        //Declare events handling
        SubscribeOnEvents(hubConnection, actionsHandler, scope);
                
        //Open connection
        await hubConnection.StartAsync(stoppingToken);
        
        //Run game
        await hubConnection.InvokeAsync("JoinTheLobby", lobbyId, player.Id, stoppingToken);
    }

    private void SubscribeOnEvents(HubConnection hubConnection, LobbyActionsHandler actionsHandler, IServiceScope scope)
    {
        hubConnection.On<Guid, IEnumerable<PlayerInGameDto>>(nameof(ILobbyActionsHandler.OnGameStarted),
            (lobbyId, players) =>
            {
                actionsHandler.OnGameStarted(lobbyId, players);
            });
        
        hubConnection.On<Guid, PlayerInGameDto>(nameof(ILobbyActionsHandler.OnGameFinished),
            (lobbyId, winner) =>
            {
                actionsHandler.OnGameFinished(lobbyId, winner);
                hubConnection.InvokeAsync("LeaveTheLobby");
                hubConnection.DisposeAsync();
                //scope.Dispose();
            });
        
        hubConnection.On<Guid, PlayerInGameDto, PlayerInGameDto>(nameof(ILobbyActionsHandler.OnStateChanged),
            (lobbyId, host, secondPlayer) =>
            {
                actionsHandler.OnStateChanged(lobbyId, host, secondPlayer);
            });
    } 

    private async Task<(PlayerDto Host, PlayerDto SecondPlayer)> GetOrCreatePlayers()
    {
        const string testHostName = "testHost";
        const string testSecondPlayerName = "testSecondPlayer";
        
        var registeredPlayers = await _httpClient.GetPlayers(0, 100);

        var testHost = registeredPlayers.FirstOrDefault(a => a.NickName == testHostName);
        var testSecondPlayer = registeredPlayers.FirstOrDefault(a => a.NickName == testSecondPlayerName);

        if (testHost == null)
        {
           var id = await _httpClient.RegisterPlayer(testHostName);
           testHost = new PlayerDto(id, testHostName);
        }

        if (testSecondPlayer == null)
        {
            var id = await _httpClient.RegisterPlayer(testSecondPlayerName);
            testSecondPlayer = new PlayerDto(id, testSecondPlayerName);
        }

        return (testHost, testSecondPlayer);
    }
}