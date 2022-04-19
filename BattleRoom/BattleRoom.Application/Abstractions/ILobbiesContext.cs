using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Abstractions;

public interface ILobbiesContext : IContext
{
    public DbSet<Lobby> Lobbies { get; set; }
}