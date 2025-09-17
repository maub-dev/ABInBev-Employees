using ABInBev.Employees.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ABInBev.Employees.Data.Context
{
    public class EmployeeDbContext
    {
        public List<Employee> Employees { get; set; } = [];
    }
}
