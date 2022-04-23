namespace BattleRoom.Domain.Entities;

/// <summary>
/// Many to many entity for Players and Lobbies.
/// </summary>
public class PlayerInLobby
{
    #region Props
    
    public Guid LobbyId { get; set; }
    
    public Guid PlayerId { get; set; }

    public bool IsHost { get; set; }

    public Lobby Lobby { get; set; }
    
    public Player Player { get; set; }
    
    #endregion

    #region Ctors

    public PlayerInLobby(Guid lobbyId, Guid playerId, bool isHost)
    {
        LobbyId = lobbyId;
        PlayerId = playerId;
        IsHost = isHost;
    }

    public PlayerInLobby() { }
    
    #endregion
}