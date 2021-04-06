// Copyright (c) PavelJedlicka. All rights reserved.

using System;
using DiscountStore.BLL.Models;

namespace DiscountStore.BLL.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<CartItem> CartItems { get; }

        IRepository<Discount> Discounts { get; }

        IRepository<Item> Items { get; }

        int SaveChanges();
    }
}