using Ardalis.Specification;
using BattleRoom.Domain.Entities;

namespace BattleRoom.Application.Specifications;

public class LobbyWithPlayersSpec : Specification<Lobby>
{
    public LobbyWithPlayersSpec(Guid lobbyId)
    {
        Query
            .Include(a => a.Players)
            .Where(a => a.Id == lobbyId);
    }
}