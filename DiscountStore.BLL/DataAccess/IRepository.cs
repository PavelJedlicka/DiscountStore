// Copyright (c) PavelJedlicka. All rights reserved.

using System;
using System.Linq;
using System.Linq.Expressions;

namespace DiscountStore.BLL.DataAccess
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(int id);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

        void Add(T entity);

        void RemoveById(int id);
    }
}