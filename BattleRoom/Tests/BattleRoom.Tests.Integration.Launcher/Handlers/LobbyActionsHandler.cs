using BattleRoom.Client.Abstractions;
using BattleRoom.Domain.Dto;
using BattleRoom.Models;

namespace BattleRoom.Tests.Integration.Launcher.Handlers;

public class LobbyActionsHandler : ILobbyActionsHandler
{
    private readonly ILogger _logger;
    private readonly PlayerDto _player;

    public LobbyActionsHandler(ILogger logger, PlayerDto player)
    {
        _logger = logger;
        _player = player;
    }

    public Task OnGameStarted(Guid lobbyId, IEnumerable<PlayerInGameDto> players)
    {
        Log("Game started");
        
        return Task.CompletedTask;
    }

    public Task OnGameFinished(Guid lobbyId, PlayerInGameDto winner)
    {
        if (winner.Id == _player.Id)
        {
            Log("Game finished. I won!");
        }
        else
        {
            Log("Game finished. I lost!");
        }
        
        return Task.CompletedTask;
    }

    public Task OnStateChanged(Guid lobbyId, PlayerInGameDto host, PlayerInGameDto secondPlayer)
    {
        if (host.Id == _player.Id)
        {
            Log($"Hy health {host.Health}. My rival health {secondPlayer.Health}");
        }
        else
        {
            Log($"Hy health {secondPlayer.Health}. My rival health {host.Health}"); 
        }
        
        return Task.CompletedTask;
    }

    private void Log(string message)
    {
        _logger.LogInformation($"{_player.NickName}: {message}");
    }
}