namespace BattleRoom.Domain.Entities;

/// <summary>
/// Registered player.
/// </summary>
public class Player
{
    #region Props
    
    public Guid Id { get; set; }

    public string NickName { get; set; }
    
    public ICollection<PlayerInLobby> Games { get; set; }
    
    #endregion

    #region Ctors

    public Player(string nickName)
    {
        Id = Guid.NewGuid();
        NickName = nickName;
    }

    protected Player() { }

    #endregion
}