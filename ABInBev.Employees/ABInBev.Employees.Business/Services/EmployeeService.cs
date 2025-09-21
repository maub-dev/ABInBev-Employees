using ABInBev.Employees.Business.Interfaces;
using ABInBev.Employees.Business.Models;
using ABInBev.Employees.Business.Models.Validators;
using Microsoft.AspNetCore.Identity;

namespace ABInBev.Employees.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeService(IEmployeeRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task AddAsync(Employee employee, string password, string emailAuthenticatedUser)
        {
            await ValidatorHelper.ValidateAsync(new EmployeeValidator(_repository, null), employee);
            var authenticatedUser = await _repository.GetByEmailAsync(emailAuthenticatedUser);
            if (!authenticatedUser.CanUseRole(employee.Role))
                throw new InvalidOperationException($"You are not allowed to create employees with role {employee.Role}.");

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
            var authenticatedUser = await _repository.GetByEmailAsync(emailAuthenticatedUser);
            if (!authenticatedUser.CanUseRole(employee.Role))
                throw new InvalidOperationException($"You are not allowed to edit employees with role {employee.Role}.");
            if (employee.Role != employeeDb.Role && !authenticatedUser.CanUseRole(employeeDb.Role))
                throw new InvalidOperationException($"You are not allowed to edit employees with role {employeeDb.Role}.");

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
            
            var authenticatedUser = await _repository.GetByEmailAsync(emailAuthenticatedUser);
            if (!authenticatedUser.CanUseRole(employee.Role))
                throw new InvalidOperationException($"You are not allowed to delete employees with role {employee.Role}.");

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
    }
}
