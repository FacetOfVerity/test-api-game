using BattleRoom.Domain.Dto;

namespace BattleRoom.Client.Abstractions;

public interface ILobbyActionsHandler
{
    Task OnGameStarted(Guid lobbyId, IEnumerable<PlayerInGameDto> players);
    
    Task OnGameFinished(Guid lobbyId, PlayerInGameDto winner);
    
    Task OnStateChanged(Guid lobbyId, PlayerInGameDto host, PlayerInGameDto secondPlayer);
}