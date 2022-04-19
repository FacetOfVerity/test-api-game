namespace BattleRoom.Domain.Entities;

public class PlayerInLobby
{
    #region Props
    
    public Guid LobbyId { get; set; }
    
    public Guid PlayerId { get; set; }

    public Lobby Lobby { get; set; }
    
    public Player Player { get; set; }
    
    #endregion

    #region Ctors

    public PlayerInLobby(Guid lobbyId, Guid playerId)
    {
        LobbyId = lobbyId;
        PlayerId = playerId;
    }
    
    #endregion
}