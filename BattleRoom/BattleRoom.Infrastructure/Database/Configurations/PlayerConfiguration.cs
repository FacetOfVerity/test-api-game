using BattleRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoom.Infrastructure.Database.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder
            .HasMany(a => a.Games)
            .WithOne(a => a.Player)
            .HasForeignKey(a => a.PlayerId);
    }
}