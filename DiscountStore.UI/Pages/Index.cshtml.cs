// Copyright (c) PavelJedlicka. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountStore.BLL.DataAccess;
using DiscountStore.BLL.Models;
using DiscountStore.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscountStore.UI.Pages
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "RazorPage convention")]
    public class IndexModel : PageModel
    {
        private readonly ICartService cartService;

        public IndexModel(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public List<CartItem> CartItems { get; set; }

        public decimal TotalPrice { get; set; }

        public void OnGet()
        {
            this.LoadCart();
        }

        public void OnPost(int id, string type)
        {
            if (type == "add")
            {
                this.cartService.Add(new CartItem()
                {
                    ItemId = id,
                });
            }
            else
            {
                this.cartService.Remove(id);
            }

            this.LoadCart();
        }

        private void LoadCart()
        {
            this.CartItems = this.cartService.GetCartItems().OrderBy(tab => tab.ItemId).ToList();
            this.TotalPrice = this.cartService.GetTotal();
        }
    }
}
