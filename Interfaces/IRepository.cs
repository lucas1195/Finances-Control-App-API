using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        DbSet<T> Table { get; }
    }
}
