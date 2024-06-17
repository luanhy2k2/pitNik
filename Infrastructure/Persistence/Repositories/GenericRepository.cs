using Core.Common;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly PitNikDbContext _context;
        public GenericRepository(PitNikDbContext context)
        {
            _context = context;
        }
        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Delete(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<BaseQuerieResponse<T>> GetAll(int pageIndex, int pageSize, Expression<Func<T, bool>> conditions)
        {
            var query = _context.Set<T>().Where(conditions);
            var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();
            return new BaseQuerieResponse<T> { Items = result, Total = total, PageIndex = pageIndex, PageSize = pageSize };
        }

        public async Task<T> getById(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            return data;
        }

        public async Task<T> Update(T entity)
        { 
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
