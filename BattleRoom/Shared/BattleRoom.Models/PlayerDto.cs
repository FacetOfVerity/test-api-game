namespace BattleRoom.Models;

public record PlayerDto
{
    public Guid Id { get; }

    public string NickName { get; }

    public PlayerDto(Guid id, string nickName)
    {
        Id = id;
        NickName = nickName;
    }
}