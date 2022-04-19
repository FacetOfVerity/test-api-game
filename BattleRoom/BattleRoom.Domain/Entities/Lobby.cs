using BattleRoom.Domain.Exceptions;

namespace BattleRoom.Domain.Entities;

/// <summary>
/// Lobby created by player (host).
/// </summary>
public class Lobby
{
    #region Props
    
    public Guid Id { get; set; }
    
    public Guid? WinnerId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }

    public DateTimeOffset? ClosedAt { get; set; }

    public bool Closed => ClosedAt.HasValue;

    public bool Started => StartedAt.HasValue;

    public ICollection<PlayerInLobby> Players { get; set; }
    
    #endregion

    #region Methods
    
    public static Lobby CreateWithHost(Guid lobbyId, Guid hostId) => new(lobbyId, hostId);

    public void Close(Guid winnerId)
    {
        EnsureNotClosed();

        if (Players.All(a => a.PlayerId != winnerId))
        {
            throw new DomainException($"Player {winnerId} isn't in lobby {Id}");
        }
        
        WinnerId = winnerId;
        ClosedAt = DateTimeOffset.UtcNow;
    }
    
    public void Start(Guid secondPlayerId)
    {
        EnsureNotClosed();
        EnsureNotStarted();
        
        StartedAt = DateTimeOffset.UtcNow;
        Players.Add(new PlayerInLobby(Id, secondPlayerId));
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

    #endregion

    #region Ctors

    private Lobby(Guid lobbyId, Guid hostId)
    {
        Id = lobbyId;
        Players = new List<PlayerInLobby>();
        Players.Add(new PlayerInLobby(Id, hostId));
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Lobby()
    {
        
    }
    
    #endregion
}