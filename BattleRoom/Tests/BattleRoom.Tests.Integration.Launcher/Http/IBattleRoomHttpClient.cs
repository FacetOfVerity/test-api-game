using BattleRoom.Models;
using Refit;

namespace BattleRoom.Tests.Integration.Launcher.Http;

public interface IBattleRoomHttpClient
{
    [Post("/api/players")]
    Task<Guid> RegisterPlayer([Query] string nickname);
    
    [Get("/api/players")]
    Task<IEnumerable<PlayerDto>> GetPlayers([Query] int offset, [Query] int count);
    
    [Get("/api/lobbies")]
    Task<IEnumerable<PlayerDto>> GetOpenedLobbies([Query] int offset, [Query] int count);
}