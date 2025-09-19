using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ABInBev.Employees.Data.Repositories
{
    public class EmployeeRepository(EmployeeDbContext dbContext) : Repository<Employee>(dbContext), IEmployeeRepository
    {
        public async Task<Employee?> GetByIdWithIncludesAsync(Guid id)
        {
            return await _dbSet.Include(x => x.Phones).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsDocumentNumberInUseAsync(string documentNumber, Guid? id)
        {
            if (id.HasValue)
                return await _dbSet.AnyAsync(x => x.DocumentNumber == documentNumber && x.Id != id);

            return await _dbSet.AnyAsync(x => x.DocumentNumber == documentNumber);
        }
    }
}
