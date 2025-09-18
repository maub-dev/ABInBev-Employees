using ABInBev.Employees.API.DTOs;
using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABInBev.Employees.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all", Name = "GetAllEmployees")]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _employeeService.GetAllAsync();
        }

        [HttpGet(Name = "GetEmployee")]
        public async Task<Employee> Get(Guid id)
        {
            return await _employeeService.GetByIdAsync(id);
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task Post(Employee employee)
        {
            await _employeeService.AddAsync(employee);
        }

        [HttpPut(Name = "UpdateEmployee")]
        public async Task Put(Employee employee)
        {
            await _employeeService.UpdateAsync(employee);
        }

        [HttpDelete(Name = "DeleteEmployee")]
        public async Task Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);
        }
    }
}
