using BattleRoom.Models;
using Refit;

namespace BattleRoom.Tests.End2End.Launcher.Http;

public interface IBattleRoomHttpClient
{
    [Post("/api/players")]
    Task<Guid> RegisterPlayer([Query] string nickname);
    
    [Get("/api/players")]
    Task<IEnumerable<PlayerDto>> GetPlayers([Query] int offset, [Query] int count);
    
    [Get("/api/lobbies")]
    Task<IEnumerable<LobbyDto>> GetOpenedLobbies([Query] int offset, [Query] int count);
}