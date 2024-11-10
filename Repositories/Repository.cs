using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finances_Control_App_API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public DbSet<T> Table => _dbSet;
        public Repository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
