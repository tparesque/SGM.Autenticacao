using SGM.Autenticacao.Domain.Interfaces.Repository;
using SGM.Autenticacao.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGM.Autenticacao.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SGMDbContext _context;

        public Repository(SGMDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task Create(T entity)
        {            
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() 
        { 
            return _context.Set<T>().CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().CountAsync(predicate);
        }
       
    }
}
