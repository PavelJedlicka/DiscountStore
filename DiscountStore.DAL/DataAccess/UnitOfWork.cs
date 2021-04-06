// Copyright (c) PavelJedlicka. All rights reserved.

using DiscountStore.BLL.DataAccess;
using DiscountStore.BLL.Models;

namespace DiscountStore.DAL.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DiscountStoreContext dbContext;
        private IRepository<CartItem> cartRepository;
        private IRepository<Item> productRepository;
        private IRepository<Discount> productDiscountRepository;

        public UnitOfWork(DiscountStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<CartItem> CartItems
        {
            get
            {
                if (this.cartRepository == null)
                {
                    this.cartRepository = new RepositoryBase<CartItem>(this.dbContext);
                }

                return this.cartRepository;
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new RepositoryBase<Item>(this.dbContext);
                }

                return this.productRepository;
            }
        }

        public IRepository<Discount> Discounts
        {
            get
            {
                if (this.productDiscountRepository == null)
                {
                    this.productDiscountRepository = new RepositoryBase<Discount>(this.dbContext);
                }

                return this.productDiscountRepository;
            }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
