using BattleRoom.Application.Abstractions;
using BattleRoom.Client.Abstractions;
using BattleRoom.Domain.Abstractions;
using BattleRoom.Models;

namespace BattleRoom.Application.Utils;

public class GameActionsPublisher : IGameActionsPublisher
{
    private readonly ISubscribersNotifier<ILobbyActionsHandler> _notifier;

    public GameActionsPublisher(ISubscribersNotifier<ILobbyActionsHandler> notifier)
    {
        _notifier = notifier;
    }

    public async Task StartGame(Guid lobbyId, IEnumerable<PlayerInGameDto> players)
    {
       var subscribers = _notifier.Subscribers(lobbyId.ToString());
       await subscribers.OnGameStarted(lobbyId, players);
    }

    public async Task FinishGame(Guid lobbyId, PlayerInGameDto winner)
    {
        var subscribers = _notifier.Subscribers(lobbyId.ToString());
        await subscribers.OnGameFinished(lobbyId, winner);
    }

    public async Task StateTransition(Guid lobbyId, PlayerInGameDto host, PlayerInGameDto secondPlayer)
    {
        var subscribers = _notifier.Subscribers(lobbyId.ToString());
        await subscribers.OnStateChanged(lobbyId, host, secondPlayer);
    }
}