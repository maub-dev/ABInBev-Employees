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
    }
}
