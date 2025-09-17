using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
