using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByIdWithIncludesAsync(Guid id);
        Task<bool> IsDocumentNumberInUseAsync(string documentNumber);
    }
}
