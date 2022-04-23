using BattleRoom.Domain.Abstractions;
using BattleRoom.Domain.Entities;

namespace BattleRoom.Domain.ValueObjects;

/// <summary>
/// Describes game process logic.
/// </summary>
public class Game
{
    private const int STEP_DELAY = 2000;
    private const int MAX_DAMAGE = 2;
    
    private readonly Random _random;
    
    private readonly Lobby _lobby;
    private readonly PlayerInGame _host;
    private readonly PlayerInGame _secondPlayer;
    private readonly IGameActionsPublisher _publisher;

    private Game(Lobby lobby, IGameActionsPublisher publisher)
    {
        _random = new Random();
        _lobby = lobby;
        _host = new PlayerInGame(_lobby.Host.Player);
        _secondPlayer = new PlayerInGame(_lobby.SecondPlayer.Player);
        _publisher = publisher;
    }

    public static Game Create(Lobby lobby, IGameActionsPublisher publisher) => new(lobby, publisher);

    /// <summary>
    /// Run game.
    /// </summary>
    public async Task Run()
    {
        //Notify players about game start
        await _publisher.StartGame(_lobby.Id, new[] {_host.GetDto(), _secondPlayer.GetDto()});

        //Waiting for game finish
        await GameLoop();

        //Define winner
        var winner = _host;
        if (!_host.IsAlive && _secondPlayer.IsAlive)
        {
            winner = _secondPlayer;
        }

        //Notify players about game outcome
        await _publisher.FinishGame(_lobby.Id, winner.GetDto());
        
        //Set lobby's state
        _lobby.Close(winner.Player.Id);
    }

    private async Task GameLoop()
    {
        while (_host.IsAlive && _secondPlayer.IsAlive)
        {
            await Task.Delay(STEP_DELAY);

            _host.TakeDamage(_random.Next(0, MAX_DAMAGE));
            _secondPlayer.TakeDamage(_random.Next(0, MAX_DAMAGE));
        
            //Notify players about intermediate state of the game
            await _publisher.StateTransition(_lobby.Id, _host.GetDto(), _secondPlayer.GetDto());
        }
    }
}