using ABInBev.Employees.API.DTOs;
using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        /// <summary>
        /// Gets all Employees registered
        /// </summary>
        /// <returns>Employees list</returns>
        [HttpGet("all", Name = "GetAllEmployees")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees.Select(x => new EmployeeDTO(x)));
        }

        /// <summary>
        /// Gets a specific Employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>The Employee</returns>
        [HttpGet(Name = "GetEmployee")]
        [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> Get(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee is null)
                return NotFound();

            return Ok(new EmployeeDTO(employee));
        }

        /// <summary>
        /// Creates a new Employee
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns>Nothing</returns>
        [HttpPost(Name = "AddEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(EmployeeDTO employee)
        {
            var emailAuthenticatedUser = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _employeeService.AddAsync(employee.ToEmployee(), employee.Password, emailAuthenticatedUser);

            return Ok();
        }

        /// <summary>
        /// Update a specific Employee
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns>Nothing</returns>
        [HttpPut(Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(EmployeeDTO employee)
        {
            var emailAuthenticatedUser = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _employeeService.UpdateAsync(employee.ToEmployee(), emailAuthenticatedUser);

            return Ok();
        }

        /// <summary>
        /// Delete a specific Employee
        /// </summary>
        /// <param name="id">Employee Id to be deleted</param>
        /// <returns></returns>
        [HttpDelete(Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var emailAuthenticatedUser = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _employeeService.DeleteAsync(id, emailAuthenticatedUser);

            return Ok();
        }
    }
}
