using ABInBev.Employees.API.DTOs;
using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABInBev.Employees.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all", Name = "GetAllEmployees")]
        public async Task<IEnumerable<EmployeeDTO>> Get()
        {
            var employees = await _employeeService.GetAllAsync();

            return employees.Select(x => new EmployeeDTO(x));
        }

        [HttpGet(Name = "GetEmployee")]
        public async Task<EmployeeDTO> Get(Guid id)
        {
            return new EmployeeDTO(await _employeeService.GetByIdAsync(id));
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task Post(EmployeeDTO employee)
        {
            await _employeeService.AddAsync(employee.ToEmployee(), employee.Password);
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
