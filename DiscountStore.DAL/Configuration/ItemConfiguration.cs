// Copyright (c) PavelJedlicka. All rights reserved.

using DiscountStore.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountStore.DAL.Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .HasKey(tab => tab.Id);

            builder
                .Property(tab => tab.Id)
                .IsRequired();

            builder
                .Property(tab => tab.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder
                .Property(tab => tab.Price)
                .IsRequired();

            builder
                .HasMany(tab => tab.CartItems)
                .WithOne();

            builder
                .HasMany(tab => tab.Discounts)
                .WithOne();
        }
    }
}
