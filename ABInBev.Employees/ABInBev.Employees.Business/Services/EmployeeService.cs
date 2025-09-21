using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Business.Models.Enums;
using ABInBev.Employees.Business.Models.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ABInBev.Employees.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public EmployeeService(IEmployeeRepository repository, 
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _repository = repository;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task AddAsync(Employee employee, string password, string emailAuthenticatedUser)
        {
            await ValidatorHelper.ValidateAsync(new EmployeeValidator(_repository, null), employee);
            await ValidateUserRole(emailAuthenticatedUser, employee.Role, "create");

            var user = new IdentityUser
            {
                UserName = employee.Email,
                Email = employee.Email,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                var errorMessage = string.Empty;
                identityResult.Errors.ToList().ForEach(x => errorMessage += $"{x.Code}: {x.Description}");
                throw new InvalidOperationException(errorMessage);
            }

            employee.UserIdentityId = user.Id;
            await _repository.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee, string emailAuthenticatedUser)
        {
            await ValidatorHelper.ValidateAsync(new EmployeeValidator(_repository, employee.Id), employee);

            var employeeDb = await _repository.GetByIdAsync(employee.Id);
            if (employeeDb is null)
                throw new InvalidOperationException($"The Employee {employee.Id} was not found.");
            await ValidateUserRole(emailAuthenticatedUser, employee.Role, "edit");
            if (employee.Role != employeeDb.Role)
                await ValidateUserRole(emailAuthenticatedUser, employeeDb.Role, "edit");

            employeeDb.BirthDate = employee.BirthDate;
            employeeDb.DocumentNumber = employee.DocumentNumber;
            employeeDb.Email = employeeDb.Email;
            employeeDb.FirstName = employee.FirstName;
            employeeDb.LastName = employee.LastName;
            employeeDb.Phone1 = employee.Phone1;
            employeeDb.Phone2 = employee.Phone2;

            await _repository.UpdateAsync(employeeDb);
        }

        public async Task DeleteAsync(Guid id, string emailAuthenticatedUser)
        {
            var employee = await GetByIdAsync(id);
            if (employee is null) return;

            await ValidateUserRole(emailAuthenticatedUser, employee.Role, "delete");

            await _repository.DeleteAsync(id);

            var user = await _userManager.FindByIdAsync(employee.UserIdentityId);
            if (user is not null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        private async Task ValidateUserRole(string emailAuthenticatedUser, EmployeeRoleEnum role, string operation)
        {
            if (_configuration["AdminUser:Email"] != emailAuthenticatedUser)
            {
                var authenticatedUser = await _repository.GetByEmailAsync(emailAuthenticatedUser);
                if (!authenticatedUser.CanUseRole(role))
                    throw new InvalidOperationException($"You are not allowed to {operation} employees with role {role}.");
            }
        }
    }
}
