namespace BattleRoom.Domain.Dto;

public record PlayerInGameDto
{
    public Guid Id { get; }

    public string NickName { get; }

    public int Health { get; }

    public PlayerInGameDto(Guid id, string nickName, int health)
    {
        Id = id;
        NickName = nickName;
        Health = health;
    }
}