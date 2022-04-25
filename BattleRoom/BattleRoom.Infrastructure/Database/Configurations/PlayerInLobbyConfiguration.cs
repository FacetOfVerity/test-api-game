using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoom.Infrastructure.Database.Configurations;

public class PlayerInLobbyConfiguration : IEntityTypeConfiguration<PlayerInLobby>
{
    public void Configure(EntityTypeBuilder<PlayerInLobby> builder)
    {
        builder.HasKey(a => new {a.LobbyId, a.PlayerId});
    }
}