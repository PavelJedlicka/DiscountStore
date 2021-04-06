// Copyright (c) PavelJedlicka. All rights reserved.

using System.Collections.Generic;
using DiscountStore.BLL.Models;

namespace DiscountStore.BLL.Services
{
    public interface ICartService
    {
        void Add(CartItem cartItem);

        void Remove(int cartItemId);

        List<CartItem> GetCartItems();

        decimal GetTotal();
    }
}
