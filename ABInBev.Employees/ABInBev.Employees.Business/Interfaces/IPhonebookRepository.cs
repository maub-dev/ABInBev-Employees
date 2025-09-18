using ABInBev.Employees.Business.Models;

namespace ABInBev.Employees.Business.Interfaces
{
    public interface IPhonebookRepository : IRepository<Phonebook>
    {
        Task<List<Phonebook>> FindByEmployeeAsync(Guid employeeId);
        Task RemoveAllPhonesFromEmployeeAsync(Guid employeeId);
    }
}
