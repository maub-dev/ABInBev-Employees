using ABInBev.Employees.Business.Models;
using System.Linq.Expressions;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
