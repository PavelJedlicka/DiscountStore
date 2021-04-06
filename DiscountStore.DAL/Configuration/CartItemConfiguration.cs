// Copyright (c) PavelJedlicka. All rights reserved.

using DiscountStore.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountStore.DAL.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
                .HasKey(tab => tab.Id);

            builder
                .Property(tab => tab.Id)
                .IsRequired();

            builder
                .Property(tab => tab.ItemId)
                .IsRequired();

            builder
                .HasOne(tab => tab.Item)
                .WithMany(tab => tab.CartItems)
                .HasForeignKey(tab => tab.ItemId);
        }
    }
}
