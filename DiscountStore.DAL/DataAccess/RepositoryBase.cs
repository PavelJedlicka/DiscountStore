// Copyright (c) PavelJedlicka. All rights reserved.

using System;
using System.Linq;
using System.Linq.Expressions;
using DiscountStore.BLL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DiscountStore.DAL.DataAccess
{
    public class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        public RepositoryBase(DiscountStoreContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected DiscountStoreContext DbContext { get; }

        public T GetById(int id)
        {
            return this.DbContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = this.DbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public virtual void Add(T entity)
        {
            this.DbContext.Add(entity);
        }

        public virtual void RemoveById(int id)
        {
            var entity = this.GetById(id);

            this.DbContext.Remove(entity);
        }
    }
}
