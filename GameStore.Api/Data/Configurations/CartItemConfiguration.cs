using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => new { ci.CartId, ci.ItemId });

        builder.HasOne(ci => ci.Cart)
               .WithMany(cart => cart.Items)
               .HasForeignKey(ci => ci.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ci => ci.Item)
               .WithMany(item => item.CartItems)
               .HasForeignKey(ci => ci.ItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}