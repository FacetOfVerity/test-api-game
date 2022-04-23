using BattleRoom.Domain.Dto;

namespace BattleRoom.Domain.Abstractions;

public interface IGameActionsPublisher
{
    Task StartGame(Guid lobbyId, IEnumerable<PlayerInGameDto> players);
    
    Task FinishGame(Guid lobbyId, PlayerInGameDto winner);
    
    Task StateTransition(Guid lobbyId, PlayerInGameDto host, PlayerInGameDto secondPlayer);
}