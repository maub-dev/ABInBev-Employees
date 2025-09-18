using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ABInBev.Employees.Data.Repositories
{
    public class PhonebookRepository(EmployeeDbContext dbContext) : Repository<Phonebook>(dbContext), IPhonebookRepository
    {
        public async Task<List<Phonebook>> FindByEmployeeAsync(Guid employeeId)
        {
            return await _dbSet.Where(x => x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task RemoveAllPhonesFromEmployeeAsync(Guid employeeId)
        {
            await _dbSet.Where(x => x.EmployeeId == employeeId).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }
    }
}
