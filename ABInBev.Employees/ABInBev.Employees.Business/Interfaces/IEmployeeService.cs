using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?> GetByEmailAsync(string email);
        Task AddAsync(Employee employee, string password, string emailAuthenticatedUser);
        Task UpdateAsync(Employee employee, string emailAuthenticatedUser);
        Task DeleteAsync(Guid id, string emailAuthenticatedUser);
    }
}
