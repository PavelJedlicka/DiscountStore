// Copyright (c) PavelJedlicka. All rights reserved.

using DiscountStore.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountStore.DAL.Configuration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder
                .HasKey(tab => tab.Id);

            builder
                .Property(tab => tab.Id)
                .IsRequired();

            builder
                .Property(tab => tab.Count)
                .IsRequired();

            builder
                .Property(tab => tab.SpecialPrice)
                .IsRequired();

            builder
                .Property(tab => tab.ItemId)
                .IsRequired();

            builder
                .HasOne(tab => tab.Item)
                .WithMany(tab => tab.Discounts)
                .HasForeignKey(tab => tab.ItemId);
        }
    }
}
