using ABInBev.Employees.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ABInBev.Employees.Data.Context
{
    public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
