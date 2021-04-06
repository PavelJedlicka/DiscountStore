// Copyright (c) PavelJedlicka. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscountStore.BLL.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }
    }
}
