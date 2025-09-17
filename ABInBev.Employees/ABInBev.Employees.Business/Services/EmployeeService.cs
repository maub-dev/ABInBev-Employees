using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Employee employee)
        {
            await Task.Yield();
            _repository.Add(employee);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Yield();
            _repository.Delete(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            await Task.Yield();
            return _repository.GetAll();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            await Task.Yield();
            return _repository.GetById(id);
        }
    }
}
