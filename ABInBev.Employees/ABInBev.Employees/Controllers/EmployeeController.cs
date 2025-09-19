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
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees.Select(x => new EmployeeDTO(x)));
        }

        [HttpGet(Name = "GetEmployee")]
        public async Task<ActionResult<EmployeeDTO>> Get(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee is null)
                return NotFound();

            return Ok(new EmployeeDTO(employee));
        }

        [HttpPost(Name = "AddEmployee")]
        public async Task<ActionResult> Post(EmployeeDTO employee)
        {
            await _employeeService.AddAsync(employee.ToEmployee(), employee.Password);

            return Ok();
        }

        [HttpPut(Name = "UpdateEmployee")]
        public async Task<ActionResult> Put(EmployeeDTO employee)
        {
            await _employeeService.UpdateAsync(employee.ToEmployee());

            return Ok();
        }

        [HttpDelete(Name = "DeleteEmployee")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);

            return Ok();
        }
    }
}
