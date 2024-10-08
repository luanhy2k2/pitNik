﻿using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<BaseQuerieResponse<T>> GetAll(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions,string orderBy);
        Task<T> getById(int id);
        Task<bool> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        IQueryable<T> GetAllQueryable();
    }
}
