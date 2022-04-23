using BattleRoom.Domain.Dto;
using BattleRoom.Domain.Entities;

namespace BattleRoom.Domain.ValueObjects;

public class PlayerInGame
{
    public Player Player { get; }

    public PlayerHealth Health { get; }
    
    public PlayerInGame(Player player)
    {
        Player = player;
        Health = new ();
    }

    public void TakeDamage(int damage) => Health.Value -= damage;
    
    public bool IsAlive => Health.Value > 0;

    public PlayerInGameDto GetDto() => new(Player.Id, Player.NickName, Health.Value);
}