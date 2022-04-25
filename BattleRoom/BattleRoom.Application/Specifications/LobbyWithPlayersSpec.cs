using Ardalis.Specification;
using BattleRoom.Domain.Entities;

namespace BattleRoom.Application.Specifications;

public class LobbyWithPlayersSpec : Specification<Lobby>
{
    public LobbyWithPlayersSpec(Guid lobbyId)
    {
        Query
            .Include(a => a.Players).ThenInclude(a => a.Player)
            .Where(a => a.Id == lobbyId);
    }
    
    public LobbyWithPlayersSpec()
    {
        Query
            .Include(a => a.Players).ThenInclude(a => a.Player);
    }
}