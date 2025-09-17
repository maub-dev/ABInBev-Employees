using ABInBev.Employees.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ABInBev.Employees.Data.Context
{
    public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
