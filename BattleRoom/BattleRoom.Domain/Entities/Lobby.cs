using BattleRoom.Domain.Exceptions;

namespace BattleRoom.Domain.Entities;

/// <summary>
/// Lobby created by player (host).
/// </summary>
public class Lobby
{
    #region Entity props
    
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }

    public DateTimeOffset? ClosedAt { get; set; }

    public ICollection<PlayerInLobby> Players { get; set; }
    
    #endregion

    #region Computed props
    
    public bool Closed => ClosedAt.HasValue;

    public bool Started => StartedAt.HasValue;

    public PlayerInLobby Host => Players.Single(a => a.IsHost);
    public PlayerInLobby SecondPlayer => Players.First(a => !a.IsHost);
    
    #endregion

    #region Logic

    public void Close(Guid winnerId)
    {
        EnsureNotClosed();
        EnsurePlayerInLobby(winnerId);

        var winner = Players.Single(a => a.PlayerId == winnerId);
        winner.IsWinner = true;
        
        ClosedAt = DateTimeOffset.UtcNow;
    }
    
    public void Start(Guid secondPlayerId)
    {
        EnsureNotClosed();
        EnsureNotStarted();
        
        StartedAt = DateTimeOffset.UtcNow;
        Players.Add(new PlayerInLobby(Id, secondPlayerId, false));
    }
    
    private void EnsureNotClosed()
    {
        if (Closed)
        {
            throw new DomainException($"Lobby {Id} already closed");
        }
    }
    
    private void EnsureNotStarted()
    {
        if (Started)
        {
            throw new DomainException($"Game in lobby {Id} already started");
        }
    }
    
    private void EnsurePlayerInLobby(Guid playerId)
    {
        if (Players.All(a => a.PlayerId != playerId))
        {
            throw new DomainException($"Player {playerId} isn't in lobby {Id}");
        }
    }

    #endregion

    #region Ctors
    
    public static Lobby CreateWithHost(Guid lobbyId, Guid hostId) => new(lobbyId, hostId);

    private Lobby(Guid lobbyId, Guid hostId)
    {
        Id = lobbyId;
        Players = new List<PlayerInLobby>();
        Players.Add(new PlayerInLobby(Id, hostId, true));
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Lobby() { }
    
    #endregion
}