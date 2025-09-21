using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> IsDocumentNumberInUseAsync(string documentNumber, Guid? id);
        Task<bool> IsEmailInUseAsync(string email, Guid? id);
        Task<Employee?> GetByEmailAsync(string email);
    }
}
