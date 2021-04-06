// Copyright (c) PavelJedlicka. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using DiscountStore.BLL.DataAccess;
using DiscountStore.BLL.Models;

namespace DiscountStore.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(CartItem cartItem)
        {
            this.unitOfWork.CartItems.Add(cartItem);
            this.unitOfWork.SaveChanges();
        }

        public void Remove(int cartItemId)
        {
            this.unitOfWork.CartItems.RemoveById(cartItemId);
            this.unitOfWork.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            return this.unitOfWork.CartItems.GetAll(tab => tab.Item.Discounts).ToList();
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            var items = this.GetCartItems().Select(tab => tab.Item);

            foreach (var groupItems in items.GroupBy(tab => tab.Id))
            {
                total += this.GetCorrectPrice(groupItems.ToList());
            }

            return total;
        }

        private decimal GetCorrectPrice(List<Item> items)
        {
            var item = items.First();
            var discountRule = item.Discounts.Where(tab => tab.Count <= items.Count).OrderByDescending(tab => tab.Count).FirstOrDefault();

            if (discountRule != null && discountRule.Count > 0)
            {
                return this.GetDiscountPrice(items, discountRule);
            }
            else
            {
                return this.GetFullPrice(items);
            }
        }

        private decimal GetDiscountPrice(List<Item> items, Discount discountRule)
        {
            // Special price
            var countSpecialPrice = items.Count() / discountRule.Count;
            var specialPrice = countSpecialPrice * discountRule.SpecialPrice;

            // Normal price for the rest of items
            var countNormalPrice = items.Count() % discountRule.Count;
            var normalPrice = countNormalPrice * items.First().Price;

            return normalPrice + specialPrice;
        }

        private decimal GetFullPrice(List<Item> items)
        {
            return items.Sum(tab => tab.Price);
        }
    }
}
