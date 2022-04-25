namespace BattleRoom.Application.Abstractions;

public interface IContext
{
    Task<int> SaveChangesAsync(CancellationToken token);
}