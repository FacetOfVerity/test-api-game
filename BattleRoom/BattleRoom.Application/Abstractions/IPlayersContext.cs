using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Abstractions;

public interface IPlayersContext : IContext
{
    public DbSet<Player> Players { get; set; }
}