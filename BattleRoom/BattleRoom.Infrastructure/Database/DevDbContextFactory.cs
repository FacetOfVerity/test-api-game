using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BattleRoom.Infrastructure.Database;

public class DevDbContextFactory : IDesignTimeDbContextFactory<BattleRoomLobbiesContext>
{
    public BattleRoomLobbiesContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BattleRoomLobbiesContext>();
        optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Database=test;");

        return new BattleRoomLobbiesContext(optionsBuilder.Options);
    }
}