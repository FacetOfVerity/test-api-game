namespace BattleRoom.Models;

public record LobbyDto
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public PlayerDto Host { get; set; }
}