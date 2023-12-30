using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<GameCard>
{
    public void Configure(EntityTypeBuilder<GameCard> builder)
    {
        builder.Property(game => game.Price)
            .HasPrecision(5, 2);
    }
}