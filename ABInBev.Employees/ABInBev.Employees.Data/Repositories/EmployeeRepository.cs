using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Data.Context;

namespace ABInBev.Employees.Data.Repositories
{
    public class EmployeeRepository(EmployeeDbContext dbContext) : Repository<Employee>(dbContext), IEmployeeRepository
    {
    }
}
