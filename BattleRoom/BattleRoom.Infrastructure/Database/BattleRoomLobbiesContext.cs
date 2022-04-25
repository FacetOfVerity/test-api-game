using System.Reflection;
using BattleRoom.Application.Abstractions;
using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Infrastructure.Database;

public class BattleRoomLobbiesContext : DbContext, ILobbiesContext, IPlayersContext
{
    public DbSet<Lobby> Lobbies { get; set; }
    
    public DbSet<Player> Players { get; set; }
    
    public DbSet<PlayerInLobby> Games { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken token)
    {
        return base.SaveChangesAsync(token);
    }
    
    public BattleRoomLobbiesContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var infrastructureAssembly = Assembly.GetAssembly(typeof(BattleRoomLobbiesContext));
        modelBuilder.ApplyConfigurationsFromAssembly(infrastructureAssembly);
    }
}