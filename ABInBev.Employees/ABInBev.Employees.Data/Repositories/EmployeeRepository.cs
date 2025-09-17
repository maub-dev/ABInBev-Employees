using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Data.Context;

namespace ABInBev.Employees.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null) 
                _dbContext.Employees.Remove(entity);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees;
        }

        public Employee? GetById(Guid id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        }
    }
}
