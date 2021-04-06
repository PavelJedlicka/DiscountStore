// Copyright (c) PavelJedlicka. All rights reserved.

using DiscountStore.BLL.Models;
using DiscountStore.DAL.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DiscountStore.DAL
{
    public class DiscountStoreContext : DbContext
    {
        public DiscountStoreContext(DbContextOptions<DiscountStoreContext> options)
        : base(options)
        {
        }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Item> Products { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());

            this.Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var vase = new Item { Id = 1, Name = "Vase", Price = 1.2M };
            var bigMug = new Item { Id = 2, Name = "Big mug", Price = 1M };
            var napkinsPack = new Item { Id = 3, Name = "Napkins pack", Price = 0.45M };

            modelBuilder.Entity<Item>().HasData(vase, bigMug, napkinsPack);

            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, ItemId = bigMug.Id, Count = 2, SpecialPrice = 1.5M },
                new Discount { Id = 2, ItemId = napkinsPack.Id, Count = 3, SpecialPrice = 0.9M });

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, ItemId = vase.Id },
                new CartItem { Id = 2, ItemId = bigMug.Id },
                new CartItem { Id = 3, ItemId = bigMug.Id },
                new CartItem { Id = 4, ItemId = bigMug.Id },
                new CartItem { Id = 5, ItemId = bigMug.Id },
                new CartItem { Id = 6, ItemId = bigMug.Id },
                new CartItem { Id = 7, ItemId = napkinsPack.Id },
                new CartItem { Id = 8, ItemId = napkinsPack.Id },
                new CartItem { Id = 9, ItemId = napkinsPack.Id });
        }
    }
}
