using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(Guid id);
        void Add(TEntity entity);
        void Delete(Guid id);
    }
}
