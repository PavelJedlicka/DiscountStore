// Copyright (c) PavelJedlicka. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscountStore.BLL.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<Discount> Discounts { get; set; }
    }
}
