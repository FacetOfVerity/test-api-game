namespace BattleRoom.Domain.ValueObjects;

public class PlayerHealth
{
    public const int MAX_PLAYER_HEALTH = 10;

    private readonly int _value;

    public int Value
    {
        get => _value;
        set
        {
            if (value > MAX_PLAYER_HEALTH)
            {
                throw new ArgumentException($"Health cannot be more than {MAX_PLAYER_HEALTH}");
            }
        }
    }

    public PlayerHealth()
    {
        _value = 10;
    }
}