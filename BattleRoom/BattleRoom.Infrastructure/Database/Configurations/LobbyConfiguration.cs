using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoom.Infrastructure.Database.Configurations;

public class LobbyConfiguration : IEntityTypeConfiguration<Lobby>
{
    public void Configure(EntityTypeBuilder<Lobby> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder
            .HasMany(a => a.Players)
            .WithOne(a => a.Lobby)
            .HasForeignKey(a => a.LobbyId);

        builder
            .Ignore(a => a.Closed)
            .Ignore(a => a.Started)
            .Ignore(a => a.Host)
            .Ignore(a => a.SecondPlayer);
    }
}